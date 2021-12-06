using System.Collections.Generic;
using System.Linq;
using DDDBlocks.Domain.Entities;
using DDDBlocks.Domain.Events;
using DDDBlocks.Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DDDBlocks.Persistence.EntityFrameworkCore.DomainEventsDispatching;

/// <summary>
/// Provides collection of domain events, using Entity Framework Core tracking.
/// </summary>
public sealed class DomainEventsProvider : IDomainEventsProvider
{
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventsProvider"/> class.
    /// </summary>
    /// <param name="context">Database context.</param>
    public DomainEventsProvider(DbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
    {
        var domainEntities = GetDomainEntities();

        return domainEntities
                .SelectMany(instance => instance.Entity.DomainEvents)
                .ToList();
    }

    /// <inheritdoc/>
    public void ClearAllDomainEvents()
    {
        var domainEntities = GetDomainEntities();

        domainEntities
            .ForEach(entity => entity.Entity.ClearDomainEvents());
    }

    /// <summary>
    /// Provides collection of domain entities, based on Entity Framework Core tracking.
    /// </summary>
    /// <returns>Collection of domain entities, that have at least one event.</returns>
    private List<EntityEntry<Entity>> GetDomainEntities()
    {
        var domainEntities = _context.ChangeTracker
            .Entries<Entity>()
            .Where(instance =>
                    instance.Entity.DomainEvents != null &&
                    instance.Entity.DomainEvents.Any())
            .ToList();

        return domainEntities;
    }
}