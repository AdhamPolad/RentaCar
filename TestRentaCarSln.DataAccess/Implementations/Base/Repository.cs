using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Entities.Base; 

namespace TestRentaCarSln.DataAccess.Implementations.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        public DbSet<TEntity> Table;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Table = _appDbContext.Set<TEntity>();
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;

        }

        public bool Delete(TEntity data)
        {
            EntityEntry<TEntity> entityEntry = Table.Remove(data);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task SoftDeleteAsync(int id)
        {
            TEntity data = await Table.FindAsync(id);
            data.IsDeleted = true;
            data.DeletedDate = DateTime.UtcNow;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            var query = Table.AsQueryable();
            return query.FirstOrDefaultAsync(x=>x.Id == id);    
        }

        public IQueryable<TEntity> Query()
        {
            return _appDbContext.Set<TEntity>();
        }

        public bool Update(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> HardDeleteAsync(int id)
        {
            TEntity data = await Table.FindAsync(id);
            data.DeletedDate = DateTime.UtcNow;
            return Delete(data);
        }
    }
}
