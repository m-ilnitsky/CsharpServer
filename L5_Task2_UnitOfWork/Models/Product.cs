using System.Collections.Generic;
using System.Text;

namespace L5_Task2_UnitOfWork.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,3} Name: {1,-30}   Price: {2, 8}   Categories: [", Id, Name, Price);

            if (ProductCategories.Count > 0)
            {
                foreach (var pc in ProductCategories)
                {
                    stringBuilder.Append(pc.Category.Name + ", ");
                }

                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }
    }
}
