﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                bookShop.AddCustomers(new List<Customer>
                {
                    new Customer{Name = "Валя", Surname = "Попов", Phone = "777-22-33", Mail = "valy@popov.net"},
                    new Customer{Name = "Коля", Surname = "Кольянов", Phone = "777-22-11", Mail = "kol@koliyanov.net"},
                    new Customer{Name = "Толя", Surname = "Тольянов", Phone = "777-22-22", Mail = "tolyan@miru.net"},
                    new Customer{Name = "Поля", Surname = "Польянова", Phone = "666-66-66", Mail = "polya@vsegda.da"},
                    new Customer{Name = "Юля", Surname = "Июля", Phone = "555-55-55", Mail = "nado-nebo@neba.net"}
                });
                bookShop.PrintCustomers();

            }

            Console.ReadKey();
        }
    }
}
