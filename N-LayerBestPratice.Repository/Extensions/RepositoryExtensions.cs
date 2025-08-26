using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N_LayerBestPratice.Repository.Abstract;
using N_LayerBestPratice.Repository.Concrete;
using N_LayerBestPratice.Repository.DbContext;
using N_LayerBestPratice.Repository.Interceptors;
using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Repository.UnitOfWork;

namespace N_LayerBestPratice.Repository.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(optionsAction: opt =>
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");

            var connectionStringOption =
                configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

            opt.UseSqlServer(connectionStringOption!.SqlConnection,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                });
            opt.AddInterceptors(new AuditDbContextInterceptor());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped((typeof(IGenericRepository<,>)), (typeof(GenericRepository<,>)));    
        return services;
    }
}