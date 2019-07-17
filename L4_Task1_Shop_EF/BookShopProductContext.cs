using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_Task1_Shop_EF
{
    public class BookShopProductContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public BookShopProductContext() : base("eBookShopConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);

            //modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasPrecision(15, 2);

            //modelBuilder.Entity<ProductCategory>().HasKey(p => p.Id);
            modelBuilder.Entity<ProductCategory>().Property(p => p.ProductId).IsRequired();
            modelBuilder.Entity<ProductCategory>().Property(p => p.CategoryId).IsRequired();
            modelBuilder.Entity<ProductCategory>().HasRequired(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);
            modelBuilder.Entity<ProductCategory>().HasRequired(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Phone).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Customer>().Property(c => c.Mail).IsOptional().HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
