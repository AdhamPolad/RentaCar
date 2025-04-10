using System.Linq.Expressions;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetByRentId(int rentId);
        Task<bool> ProcessPayment(int rentalId, decimal amount);
        Task<PaginatedResult<IEnumerable<Payment>>> GetPaymentsAsync(PaginationRequest paginationRequest, Expression<Func<Payment, bool>> filter);
    }
}
