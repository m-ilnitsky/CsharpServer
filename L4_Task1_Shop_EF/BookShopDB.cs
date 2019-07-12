using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_Task1_Shop_EF
{
    class BookShopDB : IDisposable
    {
        private bool _disposed;

        private readonly BookShopProductContext _db;

        public BookShopDB()
        {
            _db = new BookShopProductContext();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _db.Dispose();
            _disposed = true;
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("BookShopDB: _disposed == true");
            }
        }

        public void PrintCategories()
        {
            var categories = _db.Categories.ToArray();

            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }
        }

        public void PrintProducts()
        {
            var products = _db.Products.ToArray();

            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        public void PrintCustomers()
        {
            var customers = _db.Customers.ToArray();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        public void AddCategories(ICollection<string> categoryNames)
        {
            foreach (var name in categoryNames)
            {
                _db.Categories.Add(new Category { Name = name });
            }

            _db.SaveChanges();
        }

        public void AddProducts(ICollection<Product> products)
        {
            foreach (var product in products)
            {
                _db.Products.Add(product);
            }

            _db.SaveChanges();
        }

        public void AddCustomers(ICollection<Customer> customers)
        {
            foreach (var customer in customers)
            {
                _db.Customers.Add(customer);
            }

            _db.SaveChanges();
        }

        public void AddOrder(Customer customer, ICollection<Product> products)
        {
            _db.Orders.Add(new Order { CustomerId = customer.Id, ProductIds = products.Select(product => product.Id).ToArray() });
            _db.SaveChanges();
        }
    }
}
