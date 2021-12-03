using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Pipeline;
using Module = Autofac.Module;

namespace DDDBlocks.Infrastructure.Modules;

/// <summary>
/// Infrastructure module with CQRS configuration.
/// </summary>
internal sealed class MediatorModule : Module
{
    private readonly Assembly[] _assemblies;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediatorModule"/> class.
    /// </summary>
    /// <param name="applicationAssemblies">Application layer assemblies.</param>
    internal MediatorModule(params Assembly[] applicationAssemblies)
    {
        _assemblies = applicationAssemblies;
    }

    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterMediatR(_assemblies);

        var openHandlerTypes = new[]
        {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(IValidator<>),
        };

        foreach (var openHandlerType in openHandlerTypes)
        {
            builder.RegisterAssemblyTypes(_assemblies)
             .AsClosedTypesOf(openHandlerType)
             .AsImplementedInterfaces();
        }

        builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        builder.Register<ServiceFactory>(outerContext =>
        {
            var innerContext = outerContext.Resolve<IComponentContext>();

            return serviceType => innerContext.Resolve(serviceType);
        }).InstancePerLifetimeScope();
    }
}