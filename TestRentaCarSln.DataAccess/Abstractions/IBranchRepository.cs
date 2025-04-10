using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<PaginatedResult<IEnumerable<Branch>>> GetBranchsAsync(PaginationRequest paginationRequest);
    }
}

