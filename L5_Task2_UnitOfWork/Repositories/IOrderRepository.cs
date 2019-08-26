using System.Collections.Generic;

using L5_Task2_UnitOfWork.Entities;

namespace L5_Task2_UnitOfWork.Repositories
{
    interface IOrderRepository : IRepository<Order>
    {
        List<Category> GetMostFrequentCategories();
        List<Product> GetMostFrequentProducts();
        List<Customer> GetMostFrequentCustomers();
    }
}
