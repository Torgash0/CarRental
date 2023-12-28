using DataAccess.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class CarRentalDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<RentEntity> Rents { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey(); ;
        modelBuilder.Entity<UserRoleEntity>().ToTable("user_roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_role_claims");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();

        modelBuilder.Entity<UserEntity>()
                    .HasMany(user => user.Rents)
                    .WithOne(rent => rent.User);

        modelBuilder.Entity<RentEntity>()
                    .HasMany(rent => rent.Cars)
                    .WithMany(car => car.Rents);

        modelBuilder.Entity<RentEntity>()
                    .HasOne(rent => rent.Review)
                    .WithOne(review => review.Rent);
    }
}
