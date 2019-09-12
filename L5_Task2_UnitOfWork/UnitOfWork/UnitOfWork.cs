using System;
using System.Data.Entity;
using System.Transactions;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _db;

        private DbContextTransaction _transaction;

        public UnitOfWork(DbContext dbContext)
        {
            _db = dbContext;
        }

        public void Save()
        {
            _db.SaveChanges();

            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            _db.Dispose();
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            if (typeof(T) == typeof(IProductRepository))
            {
                return new ProductRepository(_db) as T;
            }
            if (typeof(T) == typeof(ICategoryRepository))
            {
                return new CategoryRepository(_db) as T;
            }
            if (typeof(T) == typeof(ICustomerRepository))
            {
                return new CustomerRepository(_db) as T;
            }
            if (typeof(T) == typeof(IProductCategoryRepository))
            {
                return new ProductCategoryRepository(_db) as T;
            }
            if (typeof(T) == typeof(IProductOrderRepository))
            {
                return new ProductOrderRepository(_db) as T;
            }
            if (typeof(T) == typeof(IOrderRepository))
            {
                return new OrderRepository(_db) as T;
            }

            throw new Exception("Неизвестный тип репозитория: " + typeof(T));
        }

        public void StartTransaction()
        {
            if (_transaction != null)
            {
                throw new TransactionException("Предыдущая транзакция не закончена!");
            }

            _transaction = _db.Database.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new TransactionException("Транзакция уже завершена!");
            }

            _transaction.Rollback();
            _transaction = null;
        }
    }
}
