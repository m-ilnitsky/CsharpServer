using System.Data.Entity;

using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class ProductCategoryRepository : BaseEfRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DbContext db) : base(db)
        {
        }
    }
}
