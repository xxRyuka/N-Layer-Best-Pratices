using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using N_LayerBestPratice.Repository.Abstract;
using N_LayerBestPratice.Repository.DbContext;

namespace N_LayerBestPratice.Repository.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private readonly static Dictionary<EntityState, Action< EntityEntry>> _behaviors = new()
    {
        { EntityState.Added, AddBehavior },
        { EntityState.Modified, UpdateBehavior }
    };

    private static void AddBehavior(EntityEntry entityEntry)
    {
        entityEntry.Property(nameof(IAuditEntity.CreatedTime)).CurrentValue = DateTime.UtcNow;
        entityEntry.Property(nameof(IAuditEntity.UpdatedTime)).IsModified = false;
    }

    private static void UpdateBehavior( EntityEntry entityEntry)
    {
        entityEntry.Property(nameof(IAuditEntity.UpdatedTime)).CurrentValue = DateTime.UtcNow;
        entityEntry.Property(nameof(IAuditEntity.CreatedTime)).IsModified = false;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        // test log dumenden
        Console.WriteLine("SaveChangesAsync is called");

        // Verileri Cekiyoruz
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries())
        {
            if (entityEntry.Entity is not IAuditEntity) continue;
            
            
            _behaviors[entityEntry.State](entityEntry);

        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}