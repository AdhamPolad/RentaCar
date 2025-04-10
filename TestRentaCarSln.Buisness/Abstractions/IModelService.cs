using TestRentaCar.Buisness.Dtos.Car;
using TestRentaCar.Buisness.Dtos.Model;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Brand;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IModelService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetModelDto>>>> GetModelsAsync(PaginationRequest paginationRequest, int? brandId, int? minYear, int? maxYear);
        Task<GenericResponceModel<GetModelDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<PostModelDto>> CreateAsync(PostModelDto postModelDto);
        Task<GenericResponceModel<bool>> DeleteAsync(int id);
        Task<GenericResponceModel<bool>> UpdateAsync(int id, UpdateModelDto updateModelDto);
        Task<GenericResponceModel<Dictionary<string, int>>> GetModelsCountByBrand();
    }
}
