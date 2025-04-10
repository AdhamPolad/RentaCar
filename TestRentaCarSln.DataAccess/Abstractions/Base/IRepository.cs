using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Abstractions.Base
{
    public interface IRepository<TEntity> : IRepositoryBase where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        Task<bool> CreateAsync(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity data);
        Task SoftDeleteAsync(int id);
        Task<bool> HardDeleteAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> Query();

    }
}
