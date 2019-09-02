using System;

namespace L5_Task2_UnitOfWork.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        T GetRepository<T>() where T : class, IRepository;
        void TransactionOfRemove<TR1, TE1, TR2, TE2>(TE1 entity1, TE2 entity2)
            where TR1 : class, IRepository, IRepository<TE1> where TE1 : class
            where TR2 : class, IRepository, IRepository<TE2> where TE2 : class;
    }
}
