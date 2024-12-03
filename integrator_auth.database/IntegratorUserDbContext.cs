using integrator_auth.database.Models;
using integrator_auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace integrator_auth.database;

public class IntegratorUserDbContext : IdentityDbContext<IntegratorUser, IntegratorRole, Guid, IdentityUserClaim<Guid>, IntegratorUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public virtual DbSet<IntegratorUser> IntegratorUsers { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public IntegratorUserDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IntegratorUser>(entity =>
        {
            entity.Property(e => e.RecCreated).HasDefaultValueSql("now() at time zone 'utc'").IsRequired();
            entity.Property(e => e.RecModified).HasDefaultValueSql("now() at time zone 'utc'").IsRequired();
            entity.HasMany(u => u.UserRoles)
               .WithOne()
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();
        });

        builder.Entity<IntegratorUserRole>(entity =>
        {
            entity.HasKey(r => new { r.UserId, r.RoleId });

            entity.HasOne(u => u.Role)
               .WithMany()
               .HasPrincipalKey(r => r.Id)
               .HasForeignKey(ur => ur.RoleId);
        });

        InitDefaultData.Init(builder);
    }
}
