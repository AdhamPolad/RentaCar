using System.Linq.Expressions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<PaginatedResult<IEnumerable<Review>>> GetReviewsAsync(PaginationRequest paginationRequest, Expression<Func<Review, bool>> filter);
    }
}
