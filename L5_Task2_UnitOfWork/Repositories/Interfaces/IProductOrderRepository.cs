using System.Collections.Generic;

using L5_Task2_UnitOfWork.Entities;
using L5_Task2_UnitOfWork.Pairs;

namespace L5_Task2_UnitOfWork.Repositories
{
    interface IProductOrderRepository : IRepository<ProductOrder>
    {
        List<ProductAndCount> GetMostFrequentProducts();
    }
}
