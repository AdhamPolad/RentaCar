using Microsoft.EntityFrameworkCore;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarSln.DataAccess.Implementations
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Customer> GetByUserId(string userId)
        {
            return await GetAll().FirstOrDefaultAsync(x=> x.UserId == userId);
        }

    }
}
