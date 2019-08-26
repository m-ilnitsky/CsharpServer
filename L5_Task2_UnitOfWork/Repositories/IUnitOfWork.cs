using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5_Task2_UnitOfWork.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        T GetRepository<T>() where T : class, IRepository;
    }
}
