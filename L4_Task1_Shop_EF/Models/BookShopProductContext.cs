using System.Data.Entity;

namespace L4_Task1_Shop_EF
{
    public class BookShopProductContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        public BookShopProductContext() : base("eBookShopConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.ProductCategories)
                .WithRequired(pc => pc.Category);

            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasPrecision(15, 2);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductCategories)
                .WithRequired(pc => pc.Product);

            modelBuilder.Entity<ProductCategory>().Property(p => p.ProductId).IsRequired();
            modelBuilder.Entity<ProductCategory>().Property(p => p.CategoryId).IsRequired();
            modelBuilder.Entity<ProductCategory>()
                .HasRequired(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);
            modelBuilder.Entity<ProductCategory>()
                .HasRequired(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Phone).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Mail).IsOptional().HasMaxLength(50);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithRequired(o => o.Customer);

            modelBuilder.Entity<ProductOrder>().Property(p => p.ProductId).IsRequired();
            modelBuilder.Entity<ProductOrder>().Property(p => p.OrderId).IsRequired();
            modelBuilder.Entity<ProductOrder>()
                .HasRequired(po => po.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductId);
            modelBuilder.Entity<ProductOrder>()
                .HasRequired(po => po.Order)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<Order>().Property(c => c.Date).IsRequired().HasColumnType("DATETIME2");
            modelBuilder.Entity<Order>().Property(c => c.CustomerId).IsRequired();
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
