using System;
using System.Collections.Generic;
using System.Linq;

namespace L4_Task1_Shop_EF
{
    class ProgramShopEF
    {
        static void Main(string[] args)
        {
            using (var bookShop = new BookShopDB())
            {
                bookShop.AddCategories(new List<string> { "книга", "журнал", "газета", "блокнот", "календарь", "карандаши", "ручки", "фломастеры" });
                bookShop.PrintCategories();

                var product1 = bookShop.GetOrCreateProduct("Газетный журнал", 100, new string[] { "газета", "журнал" });
                var product2 = bookShop.GetOrCreateProduct("Журнальная газетёнка", 150, new string[] { "газета", "журнал" });
                var product3 = bookShop.GetOrCreateProduct("Питер Таймс", 250, new string[] { "газета" });
                var product4 = bookShop.GetOrCreateProduct("Питер Пост", 250, new string[] { "журнал" });
                var product5 = bookShop.GetOrCreateProduct("Корея сегодня", 550, new string[] { "журнал" });
                bookShop.PrintProducts();
                bookShop.PrintProductCategories();

                var customer1 = bookShop.GetOrCreateCustomer("Валя", "Попов", "777-22-33", "valy@popov.net");
                var customer2 = bookShop.GetOrCreateCustomer("Коля", "Кольянов", "777-22-11", "kol@koliyanov.net");
                var customer3 = bookShop.GetOrCreateCustomer("Толя", "Тольянов", "777-22-22", "tolyan@miru.net");
                var customer4 = bookShop.GetOrCreateCustomer("Поля", "Польянова", "666-66-66", "polya@vsegda.da");
                var customer5 = bookShop.GetOrCreateCustomer("Юля", "Июля", "555-55-55", "nado-nebo@neba.net");
                bookShop.PrintCustomers();

                bookShop.AddOrder(customer1, new List<ProductOrder>
                {
                    new ProductOrder { ProductId = product1.Id, Count = 1 },
                    new ProductOrder { ProductId = product2.Id, Count = 2 },
                    new ProductOrder { ProductId = product3.Id, Count = 3 },
                    new ProductOrder { ProductId = product4.Id, Count = 4 },
                    new ProductOrder { ProductId = product5.Id, Count = 5 }
                });
                bookShop.AddOrder(customer2, new List<ProductOrder>
                {
                    new ProductOrder { ProductId = product1.Id, Count = 10},
                    new ProductOrder { ProductId = product5.Id, Count = 5 }
                });
                bookShop.AddOrder(customer3, new List<ProductOrder>
                {
                    new ProductOrder { ProductId = product1.Id, Count = 4 },
                    new ProductOrder { ProductId = product3.Id, Count = 4 },
                    new ProductOrder { ProductId = product5.Id, Count = 4 }
                });
                bookShop.AddOrder(customer1, new List<ProductOrder>
                {
                    new ProductOrder { ProductId = product1.Id, Count = 7 },
                });
                bookShop.PrintOrders();
            }

            Console.WriteLine();

            using (var db = new BookShopProductContext())
            {
                var customer = db.Customers.FirstOrDefault(c => c.Surname == "Кольянов" && c.Name == "Коля");

                if (customer == null)
                {
                    Console.WriteLine("Customer not found!");
                }
                else
                {
                    Console.WriteLine("Customer:");
                    Console.WriteLine(customer);
                    /*
                    var orders = db.Orders.Where(o => o.CustomerId == customer.Id);

                    Console.WriteLine("Orders:");
                    foreach (var order in orders)
                    {
                        Console.WriteLine(order);
                    }*/
                }
            }

            Console.ReadKey();
        }
    }
}
