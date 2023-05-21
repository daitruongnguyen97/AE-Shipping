using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shipping.Infrastructure.Data;

namespace Shipping.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<SoftDeleteEntitySaveChangesInterceptor>();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppConnection"), x => x.UseNetTopologySuite()));

            services.AddScoped<AppDbContextInitialiser>();

            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 1;
            });

            return services;
        } 
    }
}