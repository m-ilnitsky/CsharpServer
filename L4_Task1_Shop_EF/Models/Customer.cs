﻿using System.Collections.Generic;
using System.Text;

namespace L4_Task1_Shop_EF
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,4} Name: {1,-20}   Phone: {2, 10}   Mail: {3}", Id, Surname + " " + Name, Phone, Mail);

            return stringBuilder.ToString();
        }
    }
}