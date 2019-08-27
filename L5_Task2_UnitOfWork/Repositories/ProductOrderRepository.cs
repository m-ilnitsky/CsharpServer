using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using L5_Task2_UnitOfWork.Entities;
using L5_Task2_UnitOfWork.Pairs;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class ProductOrderRepository : BaseEfRepository<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(DbContext db) : base(db)
        {
        }

        public List<ProductAndCount> GetMostFrequentProducts()
        {
            int maxCount = _dbSet
                .GroupBy(o => o.Product)
                .Select(g => g.Sum(o => o.Count))
                .Max(v => v);

            return _dbSet
                    .GroupBy(o => o.Product)
                    .Select(g => new ProductAndCount
                    {
                        Product = g.Key,
                        Count = g.Sum(o => o.Count)
                    })
                    .Where(pc => pc.Count == maxCount)
                    .ToList();
        }
    }
}
