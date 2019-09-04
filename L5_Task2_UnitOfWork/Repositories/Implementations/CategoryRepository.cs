using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using L5_Task2_UnitOfWork.Entities;
using L5_Task2_UnitOfWork.Pairs;

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
                        Count = c.ProductCategories.Sum(pc => (pc.Product.ProductOrders.Count > 0) ? pc.Product.ProductOrders.Sum(po => po.Count) : 0)
                    })
                    .ToList();
        }

        public int GetCountOfProducts(Category category)
        {
            if (_dbSet.Find(category.Id) == null)
            {
                return 0;
            }

            if (category.ProductCategories.Count == 0)
            {
                return 0;
            }

            return category
                    .ProductCategories
                    .Sum(pc => pc.Product.ProductOrders.Sum(po => po.Count));
        }
    }
}
