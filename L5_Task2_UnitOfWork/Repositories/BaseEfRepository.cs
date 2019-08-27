using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace L5_Task2_UnitOfWork.Repositories
{
    public class BaseEfRepository<T> : IRepository<T> where T : class
    {
        protected DbContext _db;
        protected DbSet<T> _dbSet;

        public BaseEfRepository(DbContext dbContext)
        {
            _db = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual void Save()
        {
            _db.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void PrintAll()
        {
            foreach (var e in _dbSet.ToList())
            {
                Console.WriteLine(e);
            }
        }

        public virtual void Print(List<T> list)
        {
            foreach (var e in list)
            {
                Console.WriteLine(e);
            }
        }
    }
}
