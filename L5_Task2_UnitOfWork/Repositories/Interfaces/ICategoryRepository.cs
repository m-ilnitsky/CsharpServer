﻿using System.Collections.Generic;

using L5_Task2_UnitOfWork.Entities;
using L5_Task2_UnitOfWork.Pairs;

namespace L5_Task2_UnitOfWork.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<CategoryAndCount> GetCountsOfProductsOfCategory();
        int GetCountOfProducts(Category category);
    }
}
