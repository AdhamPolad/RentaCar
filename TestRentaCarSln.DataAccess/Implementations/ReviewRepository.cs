using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<PaginatedResult<IEnumerable<Review>>> GetReviewsAsync(PaginationRequest paginationRequest, Expression<Func<Review, bool>> filter)
        {
            var query = GetAll().Where(filter)
                                .Include(x => x.Car)
                                .ThenInclude(x => x.Model);

            int totalCount = await query.CountAsync();

            IEnumerable<Review> reviews = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                                     .Take(paginationRequest.PageSize)
                                                     .ToListAsync();

            return new PaginatedResult<IEnumerable<Review>>()
            {
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                Data = reviews
            };

        }

    }
}
