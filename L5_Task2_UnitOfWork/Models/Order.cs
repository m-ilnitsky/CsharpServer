using System;
using System.Collections.Generic;
using System.Text;

namespace L5_Task2_UnitOfWork.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,3} Customer: {1,-20} Products: [", Id, Customer.Surname + " " + Customer.Name);

            if (ProductOrders.Count > 0)
            {
                foreach (var po in ProductOrders)
                {
                    stringBuilder.Append(po.Product.Name + " " + po.Count + "шт, ");
                }

                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }
    }
}
