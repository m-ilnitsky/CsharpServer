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

        public void TransactionOfRemove<TR1, TE1, TR2, TE2>(TE1 entity1, TE2 entity2)
            where TR1 : class, IRepository, IRepository<TE1> where TE1 : class
            where TR2 : class, IRepository, IRepository<TE2> where TE2 : class
        {
            using (var dbTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var repository1 = GetRepository<TR1>();
                    var repository2 = GetRepository<TR2>();

                    repository1.Delete(entity1);
                    repository2.Delete(entity2);

                    _db.SaveChanges();

                    dbTransaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception in TransactionOfRemove: " + e.Message);

                    dbTransaction.Rollback();
                }
            }
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

        public void SaveTransaction()
        {
            if (!_startTransaction)
            {
                throw new TransactionException("Транзакция уже завершена!");
            }

            _db.SaveChanges();
            _transaction.Commit();
            _startTransaction = false;
        }
    }
}
