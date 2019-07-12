using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_Task1_Shop_EF
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int[] CategoryIds { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,3} Name: {1, 10} Categories:[", Id, Name);

            if (Categories.Count > 0)
            {
                foreach (var category in Categories)
                {
                    stringBuilder.Append(category.Name + ", ");
                }

                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            stringBuilder.Append("]");

            return base.ToString();
        }
    }
}
