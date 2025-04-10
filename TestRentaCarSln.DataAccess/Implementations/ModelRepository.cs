using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class ModelRepository : Repository<TestRentaCarDataAccess.Entities.Model>, IModelRepository
    {
        public ModelRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Dictionary<string, int>> GetModelsCountByBrand()
        {
            return await GetAll().GroupBy(x => x.Brand.Name)
                           .Select(g => new { Brand = g.Key, Count = g.Count() })
                           .ToDictionaryAsync(x=> x.Brand, x=>x.Count);

        }

        public async Task<PaginatedResult<IEnumerable<Entities.Model>>> GetModelsAsync(PaginationRequest paginationRequest, Expression<Func<Entities.Model, bool>> filter)
        {
            var query = GetAll().Where(filter)
                                .Include(x => x.Brand)
                                .AsNoTracking();

            int totalCount = await query.CountAsync();

            IEnumerable<Entities.Model> models = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                                                             .Take(paginationRequest.PageSize)
                                                                             .ToListAsync();

            return new PaginatedResult<IEnumerable<Entities.Model>>()
            {
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                Data = models
            };

        }

    }
}
