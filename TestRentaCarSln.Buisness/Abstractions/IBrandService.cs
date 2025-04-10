using TestRentaCar.Buisness.Dtos.Brand;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Brand;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IBrandService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetBrandDto>>>> GetAllAsync(PaginationRequest paginationRequest);
        Task<GenericResponceModel<GetBrandDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<PostBrandDto>> CreateAsync(PostBrandDto postBrandDto);
        Task<GenericResponceModel<bool>> DeleteAsync(int id);
        Task<GenericResponceModel<bool>> UpdateAsync(int id, UpdateBrandDto updateBrandDto);

    }
}
