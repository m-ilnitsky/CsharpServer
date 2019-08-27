using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using L5_Task2_UnitOfWork.Entities;
using L5_Task2_UnitOfWork.Pairs;

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
            if (_dbSet.Find(customer.Id) == null)
            {
                return 0;
            }

            if (customer.Orders.Count == 0)
            {
                return 0;
            }

            return customer
                    .Orders
                    .Sum(o => o.ProductOrders.Sum(po => po.Count * po.Product.Price));
        }
    }
}
