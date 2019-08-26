using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class CustomerRepository : BaseEfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext db) : base(db)
        {
        }

        public List<CustomerAndPrice> GetTotalPricesForCustomers()
        {
            return _dbSet
                    .Where(c => c.Orders.Count > 0)
                    .Select(c => new CustomerAndPrice
                    {
                        Customer = c,
                        Price = c.Orders.Sum(o => o.ProductOrders.Sum(po => po.Count * po.Product.Price))
                    })
                    .ToList<CustomerAndPrice>();
        }

        public decimal GetTotalPrice(Customer customer)
        {
            return _dbSet
                    .Find(customer)
                    .Orders
                    .Sum(o => o.ProductOrders.Sum(po => po.Count * po.Product.Price));
        }
    }
}
