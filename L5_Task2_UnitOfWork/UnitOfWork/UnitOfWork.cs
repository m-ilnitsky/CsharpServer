using System;
using System.Data.Entity;
using System.Transactions;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _db;

        private DbContextTransaction _transaction;
        private bool _startTransaction;

        public UnitOfWork(DbContext dbContext)
        {
            _db = dbContext;
        }

        public void Save()
        {
            _db.SaveChanges();

            if (_startTransaction)
            {
                _transaction.Commit();
                _startTransaction = false;
            }
        }

        public void Dispose()
        {
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
            if (_startTransaction)
            {
                throw new TransactionException("Предыдущая транзакция не закончена!");
            }

            _transaction = _db.Database.BeginTransaction();
            _startTransaction = true;
        }

        public void RollbackTransaction()
        {
            if (!_startTransaction)
            {
                throw new TransactionException("Транзакция уже завершена!");
            }

            _transaction.Rollback();
            _startTransaction = false;
        }
    }
}
