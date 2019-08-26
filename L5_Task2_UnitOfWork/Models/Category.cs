using System.Collections.Generic;
using System.Text;

namespace L5_Task2_UnitOfWork.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,4} Name: {1,-20}", Id, Name);

            return stringBuilder.ToString();
        }
    }
}
