using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L5_Task2_UnitOfWork.Entities;
using L5_Task2_UnitOfWork.Repositories;

namespace L5_Task2_UnitOfWork
{
    class ProgramUnitOfWork
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<BookShopProductContext>(new DropCreateDatabaseAlways<BookShopProductContext>());

            using (var uow = new UnitOfWork(new BookShopProductContext()))
            {
                var categoryRepository = uow.GetRepository<ICategoryRepository>();
                var productRepository = uow.GetRepository<IProductRepository>();
                var customerRepository = uow.GetRepository<ICustomerRepository>();
                var productOrderRepository = uow.GetRepository<IProductOrderRepository>();
                var productCategoryRepository = uow.GetRepository<IProductCategoryRepository>();
                var orderRepository = uow.GetRepository<IOrderRepository>();

                categoryRepository.Create(new Category { Name = "книга" });
                categoryRepository.Create(new Category { Name = "журнал" });
                categoryRepository.Create(new Category { Name = "газета" });
                categoryRepository.Create(new Category { Name = "блокнот" });
                categoryRepository.Create(new Category { Name = "календарь" });
                categoryRepository.Create(new Category { Name = "карандаши" });
                categoryRepository.Create(new Category { Name = "ручки" });
                categoryRepository.Create(new Category { Name = "фломастеры" });

                productRepository.Create(new Product { Name = "Газетный журнал", Price = 100 });
                productRepository.Create(new Product { Name = "Журнальная газетёнка", Price = 150 });
                productRepository.Create(new Product { Name = "Питер Таймс", Price = 250 });
                productRepository.Create(new Product { Name = "Питер Пост", Price = 250 });
                productRepository.Create(new Product { Name = "Корея сегодня", Price = 550 });
                productRepository.Create(new Product { Name = "Хорошая ручка", Price = 700 });

                productCategoryRepository.Create(new ProductCategory { ProductId = 1, CategoryId = 2 });
                productCategoryRepository.Create(new ProductCategory { ProductId = 1, CategoryId = 3 });
                productCategoryRepository.Create(new ProductCategory { ProductId = 2, CategoryId = 2 });
                productCategoryRepository.Create(new ProductCategory { ProductId = 2, CategoryId = 3 });
                productCategoryRepository.Create(new ProductCategory { ProductId = 3, CategoryId = 3 });
                productCategoryRepository.Create(new ProductCategory { ProductId = 4, CategoryId = 2 });
                productCategoryRepository.Create(new ProductCategory { ProductId = 5, CategoryId = 2 });
                productCategoryRepository.Create(new ProductCategory { ProductId = 6, CategoryId = 7 });

                customerRepository.Create(new Customer { Name = "Валя", Surname = "Попов", Phone = "777-22-11", Mail = "valy@popov.net", Birthday = new DateTime(2000, 1, 1) });
                customerRepository.Create(new Customer { Name = "Коля", Surname = "Колянов", Phone = "777-22-22", Mail = "kol@koliyanov.net", Birthday = new DateTime(1988, 8, 8) });
                customerRepository.Create(new Customer { Name = "Толя", Surname = "Толянов", Phone = "777-22-33", Mail = "tolyan@miru.net", Birthday = new DateTime(1977, 7, 7) });
                customerRepository.Create(new Customer { Name = "Поля", Surname = "Полянова", Phone = "555-55-55", Mail = "polya@vsegda.da", Birthday = new DateTime(1966, 6, 6) });
                customerRepository.Create(new Customer { Name = "Юля", Surname = "Июлина", Phone = "333-66-99", Mail = "nado-nebo@neba.net", Birthday = null });

                orderRepository.Create(new Order { Date = new DateTime(2018, 2, 11), CustomerId = 1 });
                orderRepository.Create(new Order { Date = new DateTime(2018, 2, 12), CustomerId = 2 });
                orderRepository.Create(new Order { Date = new DateTime(2018, 2, 13), CustomerId = 3 });
                orderRepository.Create(new Order { Date = new DateTime(2018, 2, 14), CustomerId = 1 });
                orderRepository.Create(new Order { Date = new DateTime(2018, 2, 14), CustomerId = 4 });
                orderRepository.Create(new Order { Date = new DateTime(2018, 2, 15), CustomerId = 1 });
                orderRepository.Create(new Order { Date = new DateTime(2018, 2, 15), CustomerId = 5 });

                productOrderRepository.Create(new ProductOrder { OrderId = 1, ProductId = 1, Count = 1 });
                productOrderRepository.Create(new ProductOrder { OrderId = 1, ProductId = 2, Count = 2 });
                productOrderRepository.Create(new ProductOrder { OrderId = 2, ProductId = 3, Count = 3 });
                productOrderRepository.Create(new ProductOrder { OrderId = 3, ProductId = 4, Count = 4 });
                productOrderRepository.Create(new ProductOrder { OrderId = 3, ProductId = 5, Count = 5 });
                productOrderRepository.Create(new ProductOrder { OrderId = 4, ProductId = 1, Count = 10 });
                productOrderRepository.Create(new ProductOrder { OrderId = 4, ProductId = 5, Count = 5 });
                productOrderRepository.Create(new ProductOrder { OrderId = 5, ProductId = 1, Count = 4 });
                productOrderRepository.Create(new ProductOrder { OrderId = 6, ProductId = 3, Count = 4 });
                productOrderRepository.Create(new ProductOrder { OrderId = 6, ProductId = 5, Count = 4 });
                productOrderRepository.Create(new ProductOrder { OrderId = 7, ProductId = 1, Count = 7 });

                uow.Save();

                Console.WriteLine("Categories:");
                categoryRepository.PrintAll();

                Console.WriteLine();
                Console.WriteLine("Products:");
                productRepository.PrintAll();

                Console.WriteLine();
                Console.WriteLine("Customers:");
                customerRepository.PrintAll();

                Console.WriteLine();
                Console.WriteLine("Orders:");
                orderRepository.PrintAll();

                Console.WriteLine();
                Console.WriteLine("Наиболее часто покупаемые товары:");
                var productsAndCounts = productOrderRepository.GetMostFrequentProducts();
                foreach (var pc in productsAndCounts)
                {
                    Console.WriteLine(pc);
                }

                Console.WriteLine();
                Console.WriteLine("Количество покупок по категориям:");
                var categoriesAndCounts = categoryRepository.GetCountsOfProductsOfCategory();
                foreach (var cc in categoriesAndCounts)
                {
                    Console.WriteLine(cc);
                }

                Console.WriteLine();
                var category = categoryRepository.GetById(6);
                Console.WriteLine("Количество покупок товаров из категории '{0}': {1}", category.Name, categoryRepository.GetCountOfProducts(category));

                Console.WriteLine();
                Console.WriteLine("Суммарные расходы клиентов:");
                var customersAndPrices = customerRepository.GetTotalPricesForCustomers();
                foreach (var cp in customersAndPrices)
                {
                    Console.WriteLine(cp);
                }

                Console.WriteLine();
                var customer = customerRepository.GetById(5);
                Console.WriteLine("Суммарные расходы клиента '{0} {1}': {2}", customer.Name, customer.Surname, customerRepository.GetTotalPrice(customer));

                Console.WriteLine();
                var categoryForRemove = categoryRepository.GetById(8);
                Console.WriteLine("Удаление в рамках транзакции категории '{0}':", categoryForRemove.Name);
                uow.TransactionOfRemove<CategoryRepository, Category>(categoryForRemove);
                categoryRepository.PrintAll();

                Console.WriteLine();
                var productForRemove = productRepository.GetById(6);
                Console.WriteLine("Удаление в рамках транзакции товара '{0}':", productForRemove.Name);
                uow.TransactionOfRemove<ProductRepository, Product>(productForRemove);
                productRepository.PrintAll();

                Console.ReadKey();
            }
        }
    }
}
