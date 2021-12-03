using System.Collections.Generic;
using DDDBlocks.Domain.Events;

namespace DDDBlocks.Infrastructure.DomainEventsDispatching;

/// <summary>
/// Domain events provider collect all the domain events from <see cref="Domain.Entities.Entity"/> instances.
/// </summary>
public interface IDomainEventsProvider
{
    /// <summary>
    /// Provides collection of domain events from <see cref="Domain.Entities.Entity"/> instances.
    /// </summary>
    /// <returns>Collection of unhandled domain events.</returns>
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    /// <summary>
    /// Clear all the domain events from the <see cref="Domain.Entities.Entity"/> instances.
    /// </summary>
    void ClearAllDomainEvents();
}