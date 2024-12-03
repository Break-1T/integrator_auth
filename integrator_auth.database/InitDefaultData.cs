using integrator_auth.database.Constants;
using integrator_auth.database.Models;
using integrator_auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace integrator_auth.database
{
    public static class InitDefaultData
    {
        private const string Stamp = "8787CD7E-D016-4E77-8058-36BFE8226E75";
        private const string AdminEmail = "admin@admin.com";
        private const string AdminPassword = "ABCD1234abcd@";

        /// <summary>
        /// Initializes the default data.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void Init(ModelBuilder modelBuilder)
        {
            var normalizer = new UpperInvariantLookupNormalizer();
            var hasher = new PasswordHasher<IdentityUser>();

            var identityRoles = DbConstants.Roles.Select(role =>
                new IntegratorRole
                {
                    Id = role.Value,
                    Name = role.Key,
                    NormalizedName = normalizer.NormalizeName(role.Key),
                    ConcurrencyStamp = Stamp,
                });

            modelBuilder.Entity<IntegratorRole>().HasData(identityRoles);

            modelBuilder.Entity<IntegratorUser>().HasData(
               new IntegratorUser
               {
                   Id = DbConstants.MasterAdminUserId,
                   UserName = AdminEmail,
                   NormalizedUserName = normalizer.NormalizeName(AdminEmail),
                   PasswordHash = hasher.HashPassword(null, AdminPassword),
                   EmailConfirmed = true,
                   Email = AdminEmail,
                   NormalizedEmail = normalizer.NormalizeEmail(AdminEmail),
                   ConcurrencyStamp = Stamp,
                   SecurityStamp = Stamp,
               });

            modelBuilder.Entity<IntegratorUserRole>().HasData(
                new IntegratorUserRole
                {
                    RoleId = identityRoles.First(e => e.Name == DbConstants.MasterAdminRoleName).Id,
                    UserId = DbConstants.MasterAdminUserId,
                });
        }
    }
}
