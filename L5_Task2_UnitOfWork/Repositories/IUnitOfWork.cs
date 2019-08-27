using System;

namespace L5_Task2_UnitOfWork.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        T GetRepository<T>() where T : class, IRepository;
        void TransactionOfRemove<TR, TE>(TE entity) where TR : class, IRepository, IRepository<TE> where TE : class;
    }
}
