using System;
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

                bookShop.AddProducts(new List<Product>
                {
                    new Product{Name = "Газетный журнал", Price=100, Categories = new List<Category>{new Category{Name = "газета"}, new Category{Name="журнал"}}},
                    new Product{Name = "Журнальная газетёнка", Price = 150, Categories = new List<Category>{new Category{Name = "газета"}, new Category{Name="журнал"}}},
                    new Product{Name = "Питер Таймс", Price = 250, Categories = new List<Category>{new Category{Name = "газета"}}},
                    new Product{Name = "Питер Пост", Price = 250, Categories = new List<Category>{new Category{Name = "журнал"}}},
                    new Product{Name = "Корея сегодня", Price = 550, Categories = new List<Category>{new Category{Name = "газета"}, new Category{Name="журнал"}}}
                });
                bookShop.PrintProducts();

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
