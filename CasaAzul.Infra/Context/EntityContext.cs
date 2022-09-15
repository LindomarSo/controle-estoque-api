using CasaAzul.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Infra.Context
{
    public class EntityContext : IdentityDbContext<User, Role, int,
                        IdentityUserClaim<int>, UserRole,
                        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(x => new { x.RoleId, x.UserId });

                userRole.HasOne(r => r.Role)
                        .WithMany(ur => ur.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                userRole.HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(u => u.UserId)
                        .IsRequired();
            });
        }
    }
}
