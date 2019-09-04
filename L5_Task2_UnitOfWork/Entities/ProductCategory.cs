using System.Text;

namespace L5_Task2_UnitOfWork.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Id:{0,3}  ProductId: {1,4}  CategoryId: {2,4}", Id, ProductId, CategoryId);

            return stringBuilder.ToString();
        }
    }
}
