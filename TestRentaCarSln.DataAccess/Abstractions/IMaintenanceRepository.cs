using System.Linq.Expressions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IMaintenanceRepository : IRepository<Maintenance>
    {
        Task<PaginatedResult<IEnumerable<Maintenance>>> GetMaintenancesAsync(PaginationRequest paginationRequest, Expression<Func<Maintenance, bool>> filter);
    }
}
