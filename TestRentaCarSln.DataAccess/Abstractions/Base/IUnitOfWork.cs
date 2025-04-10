using Microsoft.EntityFrameworkCore.Storage;

namespace TestRentaCarSln.DataAccess.Abstractions.Base
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        TRepository GetRepository<TRepository>() where TRepository : class, IRepositoryBase;
        Task<int> SaveAsync();
    }
}
