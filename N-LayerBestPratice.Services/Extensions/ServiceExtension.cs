    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using N_LayerBestPratice.Services.Products;

    namespace N_LayerBestPratice.Services.Extensions;

    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            
            return services;
        }
        
    }