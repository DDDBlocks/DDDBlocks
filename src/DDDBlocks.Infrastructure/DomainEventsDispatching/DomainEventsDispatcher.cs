using System.Threading.Tasks;
using MediatR;

namespace DDDBlocks.Infrastructure.DomainEventsDispatching;

/// <inheritdoc/>
internal sealed class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IMediator _mediator;
    private readonly IDomainEventsProvider _domainEventsProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventsDispatcher"/> class.
    /// </summary>
    /// <param name="mediator">MediatR DI container instance.</param>
    /// <param name="domainEventsProvider">Domain events provider.</param>
    public DomainEventsDispatcher(IMediator mediator, IDomainEventsProvider domainEventsProvider)
    {
        _mediator = mediator;
        _domainEventsProvider = domainEventsProvider;
    }

    /// <inheritdoc/>
    public async Task DispatchEventsAsync()
    {
        var domainEvents = _domainEventsProvider.GetAllDomainEvents();
        _domainEventsProvider.ClearAllDomainEvents();

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}