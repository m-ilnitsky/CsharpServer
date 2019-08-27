using System.Text;
using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Pairs
{
    public class ProductAndCount
    {
        public Product Product { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,3} Name: {1,-30}   Price: {2, 8}   Count: {3}", Product.Id, Product.Name, Product.Price, Count);

            return stringBuilder.ToString();
        }
    }
}
