﻿using System.Collections.Generic;

using L5_Task2_UnitOfWork.Entities;
using L5_Task2_UnitOfWork.Pairs;

namespace L5_Task2_UnitOfWork.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        List<CustomerAndPrice> GetTotalPricesForCustomers();
        decimal GetTotalPrice(Customer customer);
    }
}
