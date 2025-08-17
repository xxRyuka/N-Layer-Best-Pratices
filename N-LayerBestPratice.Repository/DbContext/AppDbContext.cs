using System.Reflection;
using Microsoft.EntityFrameworkCore;
using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Repository.Stores;

namespace N_LayerBestPratice.Repository.DbContext;

    //primary constructor for dependency injection , It is used to inject DbContextOptions into the AppDbContext class.
public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Store> Stores { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations for entities
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}