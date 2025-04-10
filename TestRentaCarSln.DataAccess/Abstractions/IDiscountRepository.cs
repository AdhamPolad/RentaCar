using System.Linq.Expressions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount?> GetByCode(string code);
        Task<Discount?> GetDiscountByIdAsync(int id);
        Task<PaginatedResult<IEnumerable<Discount>>> GetDiscountsAsync(PaginationRequest paginationRequest, Expression<Func<Discount, bool>> filter);
    }
}
