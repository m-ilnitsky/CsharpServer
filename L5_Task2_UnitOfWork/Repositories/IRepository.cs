using System.Collections.Generic;

namespace L5_Task2_UnitOfWork.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAll();
        T GetById(int id);
        void PrintAll();
        void Print(List<T> list);
    }
}
