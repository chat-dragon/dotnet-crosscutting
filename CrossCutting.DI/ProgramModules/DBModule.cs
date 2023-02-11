using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CrossCutting.DI.ProgramModules;

public static partial class ProgramDbServices
{
    public static IServiceCollection AddPostgresDbContext<TDbContext>(this IServiceCollection services, Assembly? migrationAssembly = null)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>((provider, options) =>
        {
            var settings = provider.GetRequiredService<AppSettings>();
            string connectionString = settings.GetConnectionString();
            var host = provider.GetService<IHostEnvironment>();
            bool dev = host.IsDevelopment();
            options.UseNpgsql(connectionString, opt =>
            {
                TimeSpan retryDelay = TimeSpan.FromMinutes(1);
                if (dev) retryDelay = TimeSpan.FromSeconds(5);
                if(migrationAssembly != null)
                {
                    opt.MigrationsAssembly(migrationAssembly.GetName().Name);
                }
                opt.CommandTimeout(60);
                opt.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: retryDelay, errorCodesToAdd: null);
            });
        }, ServiceLifetime.Scoped
        );
        return services;
    }
}
