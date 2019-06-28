namespace L2_Task1_Excel
{
    public class Person
    {
        public Person(string surname, string name, string phone, int age)
        {
            Surname = surname;
            Name = name;
            Phone = phone;
            Age = age;
        }

        public string Surname { set; get; }

        public string Name { set; get; }

        public string Phone { set; get; }

        public int Age { set; get; }
    }
}
