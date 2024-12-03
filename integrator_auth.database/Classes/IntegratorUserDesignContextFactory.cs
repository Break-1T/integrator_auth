using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace integrator_auth.database;

public class IntegratorUserDesignContextFactory : IDesignTimeDbContextFactory<IntegratorUserDbContext>
{
    public IntegratorUserDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=integrator_auth;";
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(connectionString);

        return new IntegratorUserDbContext(optionsBuilder.Options);
    }
}