using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=VigoCafeDb;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
            .HasKey(ci => new { ci.Id, ci.CartId, ci.ProductId });
            modelBuilder.Entity<CartItem>()
            .HasOne(bc => bc.Cart)
            .WithMany(p => p.CartItems)
            .HasForeignKey(pc => pc.CartId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CartItem>()
            .HasOne(pc => pc.Product)
            .WithMany(c => c.CartItems)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}