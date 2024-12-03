using integrator_auth.Models;
using integrator_auth.database.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using integrator_auth.database.Models;
using integrator_auth.database;
using Microsoft.AspNetCore.Identity;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using integrator_auth.Constants;

namespace integrator_auth.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        services.AddDatabaseServices(connectionString);

        services.AddIdentity<IntegratorUser, IntegratorRole>()
            .AddEntityFrameworkStores<IntegratorUserDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                //options.Discovery.CustomEntries.Add("identity_api", "~/identityApi");
            })
            //.AddConfigurationStore(options =>
            //{
            //    options.ConfigureDbContext = b =>
            //    {
            //        b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("integrator_auth.database.migrations")); 
            //    };
            //})
            //.AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = b =>
            //    {
            //        b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("integrator_auth.database.migrations"));
            //    };
            //})
            .AddAspNetIdentity<IntegratorUser>()
            .AddInMemoryClients([
                new ()
                {
                    ClientName = configuration.GetSection(IdentityServerContants.ClientNameEnvVariable).Value,
                    ClientId = configuration.GetSection(IdentityServerContants.ClientIdEnvVariable).Value,
                    ClientSecrets = { new Secret(configuration.GetSection(IdentityServerContants.ClientSecretEnvVariable).Value.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = [
                        "integrator_access",
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess, 
                        IdentityServerConstants.StandardScopes.OpenId ],
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                }]);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHealthChecks();

        return services;
    }
}
