using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Services.ExceptionHandlers;
using N_LayerBestPratice.Services.Filters;
using N_LayerBestPratice.Services.Products;
using N_LayerBestPratice.Services.Stores;

namespace N_LayerBestPratice.Services.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IStoreService, StoreService>();

        services.AddFluentValidationAutoValidation();
        // Register validators from the current(Service Layer) assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(expression => { }, Assembly.GetExecutingAssembly());


        services.AddScoped(typeof(NotFoundFilter<,>));
        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        // services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}