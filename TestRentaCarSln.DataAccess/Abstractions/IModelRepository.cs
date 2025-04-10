using System.Linq.Expressions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IModelRepository : IRepository<TestRentaCarDataAccess.Entities.Model>
    {
        Task<PaginatedResult<IEnumerable<TestRentaCarDataAccess.Entities.Model>>> GetModelsAsync(PaginationRequest paginationRequest, Expression<Func<Entities.Model, bool>> filter);
        Task<Dictionary<string, int>> GetModelsCountByBrand();
    }
}
