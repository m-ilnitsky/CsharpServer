using System;
using System.Collections.Generic;
using System.Text;

namespace L5_Task2_UnitOfWork.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? Birthday { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            var date = (Birthday != null) ? Birthday.Value.Date.ToString("d") : "";

            stringBuilder.AppendFormat("Id:{0,3}  Name: {1,-20}   Phone: {2, 10}   Mail: {3, 20} Birthday: {4}", Id, Surname + " " + Name, Phone, Mail, date);

            return stringBuilder.ToString();
        }
    }
}
