using System.Text;
using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Pairs
{
    public class CategoryAndCount
    {
        public Category Category { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,4} Name: {1,-20} Count: {2}", Category.Id, Category.Name, Count);

            return stringBuilder.ToString();
        }
    }
}
