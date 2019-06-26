using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2_Task1_Excel
{
    internal class PersonsToExcel
    {
        private static void Main()
        {
            var persons = new Person[7];

            persons[0] = new Person("Иванов", "Иван", "333-111", 19);
            persons[1] = new Person("Петров", "Пётр", "333-222", 23);
            persons[2] = new Person("Николаев", "Николай", "333-333", 29);
            persons[3] = new Person("Васильев", "Василий", "333-444", 31);
            persons[4] = new Person("Владимиров", "Владимир", "333-555", 37);
            persons[5] = new Person("Дмитриев", "Дмитрий", "333-666", 41);
            persons[6] = new Person("Альмухамедова", "Эльза", "737-737", 17);

            var workbook = new PersonsWorkbook(persons);

            //workbook.SaveToFile(@"C:\Users\Human\source\repos\CsharpServer\L2_Task1_Excel\persons.xlsx");
            workbook.SaveToFile("..\\..\\persons.xlsx");

            Console.WriteLine("Ok!");
        }
    }
}
