using System;

namespace L2_Task1_Excel
{
    internal class PersonsToExcel
    {
        private static void Main()
        {
            var persons = new Person[7];

            persons = new Person[]{new Person("Иванов", "Иван", "333-111", 19),
            new Person("Петров", "Пётр", "333-222", 23),
            new Person("Николаев", "Николай", "333-333", 29),
            new Person("Васильев", "Василий", "333-444", 31),
            new Person("Владимиров", "Владимир", "333-555", 37),
            new Person("Дмитриев", "Дмитрий", "333-666", 41),
            new Person("Альмухамедова", "Эльза", "737-737", 17)};

            using (var workbook = new PersonsWorkbook(persons))
            {
                workbook.SaveToFile("..\\..\\persons.xlsx");
            }

            Console.WriteLine("Ok!");
        }
    }
}
