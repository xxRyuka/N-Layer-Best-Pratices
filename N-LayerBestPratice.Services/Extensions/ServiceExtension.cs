    using System.Reflection;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using N_LayerBestPratice.Services.Products;

    namespace N_LayerBestPratice.Services.Extensions;

    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            
            services.AddFluentValidationAutoValidation();
            // Register validators from the current(Service Layer) assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(expression => {},Assembly.GetExecutingAssembly());
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
        
    }