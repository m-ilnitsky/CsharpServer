using System;
using System.Collections.Generic;
using System.Linq;

namespace L4_Task1_Shop_EF
{
    class ProgramShopEF
    {
        static void Main(string[] args)
        {
            using (var bookShop = new BookShopDb())
            {
                bookShop.AddCategories(new List<string> { "книга", "журнал", "газета", "блокнот", "календарь", "карандаши", "ручки", "фломастеры" });
                bookShop.PrintCategories();

                var product1 = bookShop.GetOrCreateProduct("Газетный журнал", 100, new[] { "газета", "журнал" });
                var product2 = bookShop.GetOrCreateProduct("Журнальная газетёнка", 150, new[] { "газета", "журнал" });
                var product3 = bookShop.GetOrCreateProduct("Питер Таймс", 250, new[] { "газета" });
                var product4 = bookShop.GetOrCreateProduct("Питер Пост", 250, new[] { "журнал" });
                var product5 = bookShop.GetOrCreateProduct("Корея сегодня", 550, new[] { "журнал" });
                bookShop.PrintProducts();
                bookShop.PrintProductCategories();

                var customer1 = bookShop.GetOrCreateCustomer("Валя", "Попов", "777-22-33", "valy@popov.net");
                var customer2 = bookShop.GetOrCreateCustomer("Коля", "Колянов", "777-22-11", "kol@koliyanov.net");
                var customer3 = bookShop.GetOrCreateCustomer("Толя", "Толянов", "777-22-22", "tolyan@miru.net");
                var customer4 = bookShop.GetOrCreateCustomer("Поля", "Полянова", "666-66-66", "polya@vsegda.da");
                var customer5 = bookShop.GetOrCreateCustomer("Юля", "Июлина", "555-55-55", "nado-nebo@neba.net");
                bookShop.PrintCustomers();

                bookShop.AddOrder(customer1, new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        ProductId = product1.Id,
                        Count = 1
                    },
                    new ProductOrder
                    {
                        ProductId = product2.Id,
                        Count = 2
                    },
                    new ProductOrder
                    {
                        ProductId = product3.Id,
                        Count = 3
                    },
                    new ProductOrder
                    {
                        ProductId = product4.Id,
                        Count = 4
                    },
                    new ProductOrder
                    {
                        ProductId = product5.Id,
                        Count = 5
                    }
                });
                bookShop.AddOrder(customer3, new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        ProductId = product1.Id,
                        Count = 10
                    },
                    new ProductOrder
                    {
                        ProductId = product5.Id,
                        Count = 5
                    }
                });
                bookShop.AddOrder(customer5, new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        ProductId = product1.Id,
                        Count = 4
                    },
                    new ProductOrder
                    {
                        ProductId = product3.Id,
                        Count = 4
                    },
                    new ProductOrder
                    {
                        ProductId = product5.Id,
                        Count = 4
                    }
                });
                bookShop.AddOrder(customer1, new List<ProductOrder>
                {
                    new ProductOrder
                    {
                        ProductId = product1.Id,
                        Count = 7
                    },
                });
                bookShop.PrintOrders();
            }

            Console.WriteLine();
            Console.WriteLine("<<<== Поиск, редактирование, удаление данных ==>>>");
            Console.WriteLine();

            using (var db = new BookShopProductContext())
            {
                Console.WriteLine("<<< Поиск данных >>>");
                Console.WriteLine();

                var customer = db.Customers.FirstOrDefault(c => c.Surname == "Толянов" && c.Name == "Толя");

                if (customer == null)
                {
                    Console.WriteLine("Customer not found!");
                }
                else
                {
                    Console.WriteLine("Customer:");
                    Console.WriteLine(customer);

                    var orders = db.Orders
                        .Where(o => o.CustomerId == customer.Id)
                        .ToList();

                    Console.WriteLine("Orders:");
                    foreach (var order in orders)
                    {
                        Console.WriteLine(order);
                    }

                    Console.WriteLine();
                    Console.WriteLine("<<< Редактирование данных >>>");
                    Console.WriteLine();

                    customer.Phone = "123-0-321";
                    customer.Mail = "valya-new@mail.ru";

                    Console.WriteLine("<<< Удаление данных >>>");
                    Console.WriteLine();

                    var badCustomer = db.Customers.FirstOrDefault(c => c.Surname == "Колянов" && c.Name == "Коля");
                    if (badCustomer != null)
                    {
                        db.Customers.Remove(badCustomer);
                        db.SaveChanges();
                    }

                    var customers = db.Customers.ToList();
                    foreach (var c in customers)
                    {
                        Console.WriteLine(c);
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("<<<== Запросы с помощью LINQ ==>>>");
            Console.WriteLine();

            using (var db = new BookShopProductContext())
            {
                Console.WriteLine("<<< Самый популярный продукт >>>");

                var popularProduct = db.ProductOrders
                    .GroupBy(o => o.Product)
                    .Select(g => new
                    {
                        Product = g.Key,
                        Count = g.Sum(o => o.Count)
                    })
                    .OrderByDescending(p => p.Count)
                    .FirstOrDefault();

                if (popularProduct == null)
                {
                    Console.WriteLine("Самый популярный продукт не найден");
                }
                else
                {
                    Console.WriteLine("Самый популярный продукт: {0} ({1} заказов)", popularProduct.Product.Name, popularProduct.Count);
                }

                Console.WriteLine();
                Console.WriteLine("<<< Расходы клиентов >>>");

                var customerExpenses1 = db.Customers
                    .Join(db.Orders,
                        c => c.Id,
                        o => o.CustomerId,
                        (c, o) => new
                        {
                            Customer = c,
                            OrderId = o.Id
                        })
                    .Join(db.ProductOrders,
                        c => c.OrderId,
                        po => po.OrderId,
                        (c, po) => new
                        {
                            c.Customer,
                            Expenses = po.Count * po.Product.Price
                        })
                    .GroupBy(c => c.Customer)
                    .Select(g => new
                    {
                        Customer = g.Key,
                        Expenses = g.Sum(c => c.Expenses)
                    })
                    .ToList();

                var customerExpenses2 = db.Customers
                    .Where(c => c.Orders.Count > 0)
                    .Select(c => new
                    {
                        Customer = c,
                        Expenses = c.Orders.Sum(o => o.ProductOrders.Sum(po => po.Count * po.Product.Price))
                    });

                foreach (var customer in customerExpenses1)
                {
                    Console.WriteLine("Клиент:[ id={0} ФИО={1} ] Расходы: {2} руб.",
                        customer.Customer.Id,
                        customer.Customer.Surname + " " + customer.Customer.Name,
                        customer.Expenses);
                }

                Console.WriteLine();

                foreach (var customer in customerExpenses2)
                {
                    Console.WriteLine("Клиент:[ id={0} ФИО={1} ] Расходы: {2} руб.",
                        customer.Customer.Id,
                        customer.Customer.Surname + " " + customer.Customer.Name,
                        customer.Expenses);
                }

                Console.WriteLine();
                Console.WriteLine("<<< Количество покупок по категориям >>>");

                var categoryProductCounts1 = db.Categories
                    .Join(db.ProductCategories,
                        c => c.Id,
                        pc => pc.CategoryId,
                        (c, pc) => new
                        {
                            Category = c,
                            pc.ProductId
                        })
                    .Join(db.ProductOrders,
                        c => c.ProductId,
                        po => po.ProductId,
                        (c, po) => new
                        {
                            c.Category,
                            po.Count
                        })
                    .GroupBy(c => c.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        Count = g.Sum(c => c.Count)
                    })
                    .ToList();

                var categoryProductCounts2 = db.Categories
                    .Where(c => c.ProductCategories.Count > 0)
                    .Select(c => new
                    {
                        Category = c,
                        Count = c.ProductCategories.Sum(pc => pc.Product.ProductOrders.Sum(po => po.Count))
                    });

                foreach (var category in categoryProductCounts1)
                {
                    Console.WriteLine("Категория:[ id={0} Название={1} ] Заказано товаров: {2} шт.",
                        category.Category.Id,
                        category.Category.Name,
                        category.Count);
                }

                Console.WriteLine();

                foreach (var category in categoryProductCounts2)
                {
                    Console.WriteLine("Категория:[ id={0} Название={1} ] Заказано товаров: {2} шт.",
                        category.Category.Id,
                        category.Category.Name,
                        category.Count);
                }
            }

            Console.ReadKey();
        }
    }
}
