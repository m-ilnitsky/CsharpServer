using System;

namespace L5_Task2_UnitOfWork.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        T GetRepository<T>() where T : class, IRepository;
        void StartTransaction();
        void RollbackTransaction();
    }
}
