using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCar.Buisness.Dtos.Review;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IRentalService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetRentalDto>>>> GetAllAsync(PaginationRequest paginationRequest, int? carId, int? customerId, RentalStatus? rentalStatus);
        Task<GenericResponceModel<RentalReponceDto>> ReturnCar(int rentalId);
        Task<GenericResponceModel<RentalReponceDto>> RentCar(PostRentalDto postRentalDto);
        Task<GenericResponceModel<RentalReponceDto>> CancelRental(int rentalid);
        Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType, int id);
        Task<GenericResponceModel<GetRentalDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<RentalReponceDto>> UpdateAsync(UpdateRentalDto updateRentalDto);
    }
}
