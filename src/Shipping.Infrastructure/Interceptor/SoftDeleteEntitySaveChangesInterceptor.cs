using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

#nullable enable
namespace Domain;

public class SoftDeleteEntitySaveChangesInterceptor : SaveChangesInterceptor
{

    public SoftDeleteEntitySaveChangesInterceptor()
    {
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DeleteEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DeleteEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void DeleteEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.Entity.Deleted = true;
                entry.Entity.DeletedDate = DateTime.UtcNow;
                entry.State = EntityState.Modified;
            }
        }
    }
}