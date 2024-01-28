using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CodeLearn.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor<TId> : SaveChangesInterceptor
{
    private readonly IUser _user;
    private readonly TimeProvider _dateTime;

    public AuditableEntityInterceptor(
        IUser user,
        TimeProvider dateTime)
    {
        _user = user;
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var auditableEntries = context.ChangeTracker.Entries()
            .Where(e => typeof(BaseAuditableEntity<TId>).IsAssignableFrom(e.Entity.GetType()));

        foreach (var entry in auditableEntries)
        {
            var baseEntity = (BaseAuditableEntity<TId>)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                baseEntity.CreatedBy = _user.Id;
                baseEntity.Created = _dateTime.GetUtcNow();
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                baseEntity.LastModifiedBy = _user.Id;
                baseEntity.LastModified = _dateTime.GetUtcNow();
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}