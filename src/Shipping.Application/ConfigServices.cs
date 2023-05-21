using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Shipping.Infrastructure;
using Shipping.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shipping.Behaviours;

namespace Shipping.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMemoryCache();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AppConnection"), x => x.UseNetTopologySuite()));

        services.AddScoped<IMemoryCache, MemoryCache>();
        services.AddScoped(typeof(ILinqRepository<>), typeof(LinqRepository<>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}
