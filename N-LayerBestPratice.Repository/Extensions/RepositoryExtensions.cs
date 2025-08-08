using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N_LayerBestPratice.Repository.DbContext;

namespace N_LayerBestPratice.Repository.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(optionsAction: opt =>
        {

            var connectionString = configuration.GetConnectionString("SqlConnection");
            
            var connectionStringOption = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            
            opt.UseSqlServer(connectionStringOption!.SqlConnection,sqlServerOptionsAction: sqlOptions =>
            {
              sqlOptions.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);  
            }) ;
            

        });
        return services;
    }
}