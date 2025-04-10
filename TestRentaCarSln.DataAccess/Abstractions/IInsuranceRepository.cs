using System.Linq.Expressions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IInsuranceRepository : IRepository<Insurance>
    {
        Task<PaginatedResult<IEnumerable<Insurance>>> GetInsurances(PaginationRequest paginationRequest, Expression<Func<Insurance, bool>> filter);
    }
}
