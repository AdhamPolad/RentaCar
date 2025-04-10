using Microsoft.EntityFrameworkCore;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public BranchRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<PaginatedResult<IEnumerable<Branch>>> GetBranchsAsync(PaginationRequest paginationRequest)
        {
            var query = GetAll();

            int totalCount = await query.CountAsync();

            IEnumerable<Branch> branches = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                                .Take(paginationRequest.PageSize)
                                                .ToListAsync();

            return new PaginatedResult<IEnumerable<Branch>>()
            {
                Data = branches,
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };

        }

    }
}
