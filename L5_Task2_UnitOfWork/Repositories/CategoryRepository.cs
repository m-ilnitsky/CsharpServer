using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Repositories
{
    class CategoryRepository : BaseEfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext db) : base(db)
        {
        }

        public List<CategoryAndCount> GetCountsOfProductsOfCategory()
        {
            return _dbSet
                    .Where(c => c.ProductCategories.Count > 0)
                    .Select(c => new CategoryAndCount
                    {
                        Category = c,
                        Count = c.ProductCategories.Sum(pc => pc.Product.ProductOrders.Sum(po => po.Count))
                    })
                    .ToList<CategoryAndCount>();
        }

        public int GetCountOfProducts(Category category)
        {
            return _dbSet
                    .Find(category)
                    .ProductCategories
                    .Sum(pc => pc.Product.ProductOrders.Sum(po => po.Count));
        }
    }
}
