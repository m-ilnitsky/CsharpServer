using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _db;

        public UnitOfWork(DbContext dbContext)
        {
            _db = dbContext;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            if (typeof(T) == typeof())
            {

            }
        }
    }
}
