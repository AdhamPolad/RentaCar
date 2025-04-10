using TestRentaCar.Buisness.Dtos.Insurance;
using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCar.Buisness.Dtos.Review;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IReviewService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetReviewDto>>>> GetAllAsync(PaginationRequest paginationRequest, int? carId, ReviewRating? reviewRating);
        Task<GenericResponceModel<RentalReponceDto>> CreateAsync(PostReviewDto postReviewDto);
        Task<GenericResponceModel<bool>> DeleteAsync(int id, DeleteType deleteType);
        Task<GenericResponceModel<bool>> UpdateAsync(UpdateReviewDto updateReviewDto);
        Task<GenericResponceModel<GetReviewDto>> GetById(int id);
    }
}
