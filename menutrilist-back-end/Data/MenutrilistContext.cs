using Microsoft.EntityFrameworkCore;
using Menutrilist.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Menutrilist.Domain.FatSecret;

#nullable disable

namespace Menutrilist.Data
{
    public partial class MenutrilistContext : IdentityDbContext<AspNetUser, AspNetRole, int, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
    {
        public MenutrilistContext()
        {
        }

        public MenutrilistContext(DbContextOptions<MenutrilistContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetLinkToken> AspNetLinkTokens { get; set; }
        public virtual DbSet<AspNetRefreshToken> AspNetRefreshTokens { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodNutritionServing> FoodNutritionServings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetUserLogin>()
                .HasKey(entity => new { entity.LoginProvider, entity.ProviderKey });
            modelBuilder.Entity<AspNetUserRole>()
                .HasKey(entity => new { entity.RoleId, entity.UserId});
            modelBuilder.Entity<AspNetUserToken>()
                .HasKey(entity => new { entity.UserId, entity.LoginProvider, entity.Name });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}