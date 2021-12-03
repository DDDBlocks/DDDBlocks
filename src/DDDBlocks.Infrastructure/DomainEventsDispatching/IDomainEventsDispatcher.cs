using System.Threading.Tasks;

namespace DDDBlocks.Infrastructure.DomainEventsDispatching;

/// <summary>
/// A mechanism for domain events handling.
/// </summary>
internal interface IDomainEventsDispatcher
{
    /// <summary>
    /// Rises all the domain events from <see cref="Domain.Entities.Entity"/> instances.
    /// </summary>
    /// <returns>Running task with domain events handling.</returns>
    Task DispatchEventsAsync();
}