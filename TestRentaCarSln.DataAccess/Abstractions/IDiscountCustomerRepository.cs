using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Abstractions.Base;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IDiscountCustomerRepository : IRepository<DiscountCustomer>
    {
        Task<DiscountCustomer?> GetByCustomerIdAndDiscountIdAsync(int customerId, int discountId);
    }
}
