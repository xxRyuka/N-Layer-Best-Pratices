using Microsoft.EntityFrameworkCore.Diagnostics;

namespace N_LayerBestPratice.Repository.DbContext;

public class TestIntercreptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        // test log dumenden
        Console.WriteLine("SaveChangesAsync is called");
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries())
        {
            
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}