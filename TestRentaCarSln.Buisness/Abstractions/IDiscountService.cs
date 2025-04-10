using TestRentaCar.Buisness.Dtos.Discount;
using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IDiscountService
    {
        Task<GenericResponceModel<bool>> CreateAsync(PostDiscountDto postDiscountDto);
        Task<GenericResponceModel<RentalReponceDto>> ApplyAsync(string code, int rentalId);
        Task<GenericResponceModel<GetDiscountDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetDiscountDto>>>> GetAllAsync(PaginationRequest paginationRequest, bool? isActive);
        Task<GenericResponceModel<bool>> UpdateAsync(UpdateDiscountDto updateDiscountDto);
        Task<GenericResponceModel<bool>> DeleteAsync(int id);
        Task<GenericResponceModel<bool>> DeActiveDiscount(int id);
    }
}
