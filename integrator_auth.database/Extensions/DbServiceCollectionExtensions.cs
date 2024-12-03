using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace integrator_auth.database.Extensions;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        services.AddDbContextPool<IntegratorUserDbContext>(contextOptions =>
        {
            contextOptions.UseNpgsql(connectionString, npgOptions =>
            {
                npgOptions.EnableRetryOnFailure();
            });
        });

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            using var dbContext = scope.ServiceProvider.GetRequiredService<IntegratorUserDbContext>();
            dbContext.Database.Migrate();
        }

        return services;
    }
}
