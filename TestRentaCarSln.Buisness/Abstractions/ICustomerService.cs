using TestRentaCar.Buisness.Dtos.Customer;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface ICustomerService
    {
        Task<GenericResponceModel<IEnumerable<GetCustomerDto>>> GetAllAsync();
        Task<GenericResponceModel<GetCustomerDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType, int id);
        Task<GenericResponceModel<bool>> UpdateAsync(UpdateCustomerDto updateCustomerDto);

    }
}
