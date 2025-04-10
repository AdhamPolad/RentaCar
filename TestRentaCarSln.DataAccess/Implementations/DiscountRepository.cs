using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Discount?> GetByCode(string code)
        {
            return await GetAll().FirstOrDefaultAsync(x=> x.Code == code);
        }

        public async Task<Discount?> GetDiscountByIdAsync(int id)
        {
            return await GetAll().Where(x => x.Id == id)
                                 .Include(x => x.DiscountCustomers)
                                 .FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<IEnumerable<Discount>>> GetDiscountsAsync(PaginationRequest paginationRequest, Expression<Func<Discount, bool>> filter)
        {
            var query = GetAll().Where(filter)
                                .Include(x => x.DiscountCustomers)
                                .AsNoTracking();

            int totalCount = await query.CountAsync();

            var rentals = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                     .Take(paginationRequest.PageSize)
                                     .ToListAsync();

            return new PaginatedResult<IEnumerable<Discount>>()
            {
                TotalCount = totalCount,
                Data = rentals,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };

        }

    }
}
