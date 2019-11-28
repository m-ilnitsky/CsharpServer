using System;

namespace Test_DateTimeOffset_MySQL
{
    public class SimpleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTimeOffset CreateDateWithOffset { get; set; }
    }
}
