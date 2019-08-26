using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class OrderRepository : BaseEfRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext db) : base(db)
        {
        }

        List<Category> GetMostFrequentCategories()
        {

        }

        List<Product> GetMostFrequentProducts()
        {
            return _dbSet
                    .Where(o => o.ProductOrders.Count > 0)
                    .Select(o => new { o.ProductOrders }               )
                    .OrderByDescending(p => p.Count)
                    .FirstOrDefault();
        }

        List<Customer> GetMostFrequentCustomers()
        {

        }
    }
}
