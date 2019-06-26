namespace L2_Task1_Excel
{
    public class Person
    {
        public Person(string family, string name, string phone, int age)
        {
            Family = family;
            Name = name;
            Phone = phone;
            Age = age;
        }

        public string Family { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public int Age { set; get; }
    }
}
