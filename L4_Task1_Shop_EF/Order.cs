using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_Task1_Shop_EF
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public int[] ProductIds { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
