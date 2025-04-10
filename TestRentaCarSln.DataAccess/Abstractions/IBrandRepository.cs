using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.DataAccess.Abstractions
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<PaginatedResult<IEnumerable<Brand>>> GetBrandsAsync(PaginationRequest paginationRequest);
    }
}
