using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Payment> GetByRentId(int rentId)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.RentalId == rentId);
        }

        public async Task<bool> ProcessPayment(int rentalId, decimal amount)
        {
            Payment payment = await GetByRentId(rentalId);
            
            if(payment is null)
                return false;
            payment.Amount = amount;
            payment.Status = PaymentStatus.Completed.ToString();
            Update(payment);
            return true;
        }

        public async Task<PaginatedResult<IEnumerable<Payment>>> GetPaymentsAsync(PaginationRequest paginationRequest, Expression<Func<Payment, bool>> filter)
        {
            var query = GetAll().Where(filter)
                .Include(x=>x.Rental)
                .AsNoTracking();

            int totalCount = await query.CountAsync();

            IEnumerable<Payment> payments = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                                 .Take(paginationRequest.PageSize)
                                                 .ToListAsync();

            return new PaginatedResult<IEnumerable<Payment>>
            {
                Data = payments,
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };

        }


    }
}
