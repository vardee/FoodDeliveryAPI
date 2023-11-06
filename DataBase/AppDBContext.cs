using backendTask.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;

namespace backendTask.DataBase.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<OrderedDishes> OrderedDishes { get; set; }
        public DbSet<BlackListTokens> BlackListTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Email);

            modelBuilder.Entity<Cart>().HasKey(c => new { c.DishId, c.UserId });
            modelBuilder.Entity<Order>().HasKey(c => new { c.UserId, c.OrderId });
            modelBuilder.Entity<Rating>().HasKey(c => new { c.DishId, c.UserId });

            modelBuilder.Entity<OrderedDishes>().HasKey(od => new { od.OrderId, od.DishId });

            base.OnModelCreating(modelBuilder);
        }
    }

}