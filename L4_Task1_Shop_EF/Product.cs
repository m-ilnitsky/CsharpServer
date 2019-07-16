using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_Task1_Shop_EF
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,3} Name: {1, 10}  Price: {2, 8}", Id, Name, Price);

            /*if (Categories.Count > 0)
            {
                foreach (var category in Categories)
                {
                    stringBuilder.Append(category.Name + ", ");
                }

                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            stringBuilder.Append("]");*/

            return base.ToString();
        }
    }
}
