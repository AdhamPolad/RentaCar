using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Abstractions.Base
{
    public interface IUnitOfWork : IDisposable
    {
        public TRepository GetRepository<TRepository>() where TRepository : class, IRepositoryBase;
        public Task<int> SaveAsync();


    }
}
