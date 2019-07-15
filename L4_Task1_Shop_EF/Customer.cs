﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_Task1_Shop_EF
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Mail { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,4} Name: {1,-20}   Phone: {2, 10}   Mail: {3}", Id, Surname + " " + Name, Phone, Mail);

            return stringBuilder.ToString();
        }
    }
}