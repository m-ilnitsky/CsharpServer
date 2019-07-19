using System;
using System.Collections.Generic;
using System.Linq;

namespace L4_Task1_Shop_EF
{
    class BookShopDb : IDisposable
    {
        private bool _disposed;

        private readonly BookShopProductContext _db;

        public BookShopDb()
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
                throw new ObjectDisposedException("BookShopDb: _disposed == true");
            }
        }

        public void PrintCategories()
        {
            var categories = _db.Categories.ToArray();

            Console.WriteLine();
            Console.WriteLine("Categories:");

            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }
        }

        public void PrintProducts()
        {
            var products = _db.Products.ToArray();

            Console.WriteLine();
            Console.WriteLine("Products:");

            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        public void PrintProductCategories()
        {
            var productCategories = _db.ProductCategories.ToArray();

            Console.WriteLine();
            Console.WriteLine("ProductCategories:");

            foreach (var productCategory in productCategories)
            {
                Console.WriteLine(productCategory);
            }
        }

        public void PrintCustomers()
        {
            var customers = _db.Customers.ToArray();

            Console.WriteLine();
            Console.WriteLine("Customers:");

            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        public void PrintOrders()
        {
            var orders = _db.Orders.ToArray();

            Console.WriteLine();
            Console.WriteLine("Orders:");

            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }

        public Category GetOrCreateCategory(string categoryName)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                category = new Category() { Name = categoryName };
                _db.Categories.Add(category);
                _db.SaveChanges();
            }

            return category;
        }

        public void AddCategories(ICollection<string> categoryNames)
        {
            foreach (var name in categoryNames)
            {
                GetOrCreateCategory(name);
            }
        }

        public Product GetOrCreateProduct(string name, decimal price, string[] categoryNames)
        {
            var product = _db.Products.FirstOrDefault(c => c.Name == name);

            if (product == null)
            {
                product = new Product() { Name = name, Price = price };

                _db.Products.Add(product);

                var categories = new Category[categoryNames.Length];
                var productCategories = new ProductCategory[categoryNames.Length];

                for (var i = 0; i < categoryNames.Length; i++)
                {
                    categories[i] = GetOrCreateCategory(categoryNames[i]);


                    productCategories[i] = new ProductCategory() { ProductId = product.Id, CategoryId = categories[i].Id };
                    _db.ProductCategories.Add(productCategories[i]);
                }

                _db.SaveChanges();
            }

            return product;
        }

        public Customer GetOrCreateCustomer(string name, string surname, string phone, string mail)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Name == name && c.Surname == surname);

            if (customer == null)
            {
                customer = new Customer { Name = name, Surname = surname, Phone = phone, Mail = mail };

                _db.Customers.Add(customer);
                _db.SaveChanges();
            }

            return customer;
        }

        public void AddCustomers(ICollection<Customer> customers)
        {
            foreach (var customer in customers)
            {
                GetOrCreateCustomer(customer.Name, customer.Surname, customer.Phone, customer.Mail);
            }
        }

        public void AddOrder(Customer customer, ICollection<ProductOrder> productOrders)
        {
            var order = new Order { CustomerId = customer.Id, Date = DateTime.Now };
            _db.Orders.Add(order);

            foreach (var productOrder in productOrders)
            {
                productOrder.OrderId = order.Id;
                _db.ProductOrders.Add(productOrder);
            }

            _db.SaveChanges();
        }
    }
}
