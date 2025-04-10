using Microsoft.EntityFrameworkCore;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    internal class DiscountCustomerRepository : Repository<DiscountCustomer>, IDiscountCustomerRepository
    {
        public DiscountCustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<DiscountCustomer?> GetByCustomerIdAndDiscountIdAsync(int customerId, int discountId)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.CustomerId == customerId && x.DiscountId == discountId);
        }

    }
}
