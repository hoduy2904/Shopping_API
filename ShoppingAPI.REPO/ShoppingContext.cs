using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common;
using ShoppingAPI.Data.Models;

namespace ShoppingAPI.REPO
{
    public class ShoppingContext : DbContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingDeliveryAddress> InfomationUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
            //Seed Role
            var role = new Role
            {
                Id=1,
                Name = "SuperAdmin",
                Created=DateTime.Now
            };
            
            modelBuilder.Entity<Role>().HasData(role);

            //Seed User
            var user = new User
            {
                Id=1,
                Username = "Admin",
                PasswordHash = StringHashing.Hash("admin"),
                LastName="Admin",
                Created = DateTime.Now
            };
            modelBuilder.Entity<User>().HasData(user);
            //Seed Role

            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id=1,
                RoleId = role.Id,
                UserId = user.Id,
                Created = DateTime.Now
            });
        }
    }
}
