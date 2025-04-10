using Microsoft.EntityFrameworkCore;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarSln.DataAccess.Implementations
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<PaginatedResult<IEnumerable<Brand>>> GetBrandsAsync(PaginationRequest paginationRequest)
        {
            var query = GetAll().AsNoTracking();

            int totalCount = await query.CountAsync();

            IEnumerable<Brand> brands = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                             .Take(paginationRequest.PageSize)
                                             .ToListAsync();


            return new PaginatedResult<IEnumerable<Brand>>()
            {
                TotalCount = totalCount,
                Data = brands,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };
        }

    }
}
