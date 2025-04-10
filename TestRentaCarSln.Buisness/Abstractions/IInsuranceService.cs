using TestRentaCar.Buisness.Dtos.Insurance;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IInsuranceService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetInsuranceDto>>>> GetAllAsync(PaginationRequest paginationRequest, CarInsuranceType? carInsuranceType, int? minPrice, int? maxPrice);
        Task<GenericResponceModel<bool>> CreateAsync(PostInsuranceDto postInsuranceDto);
        Task<GenericResponceModel<bool>> DeleteAsync(int id, DeleteType deleteType);
        Task<GenericResponceModel<bool>> UpdateAsync(UpdateInsuranceDto updateInsuranceDto);
        Task<GenericResponceModel<GetInsuranceDto>> GetById(int id);

    }
}
