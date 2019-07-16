using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_Task1_Shop_EF
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }
    }
}
