using TestRentaCar.Buisness.Dtos.Car;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCarSln.Buisness.Abstractions
{
    public interface ICarService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetCarDto>>>> GetAviableCarsAsync(PaginationRequest paginationRequest,
                                                int? modelId, CarCatagory? carCatagory, int? enginId);
        Task<GenericResponceModel<GetCarDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<PostCarDto>> CreateAsync(PostCarDto postCarDto);
        Task<GenericResponceModel<bool>> DeleteAsync(int id);
        Task<GenericResponceModel<GetCarDto>> GetCheapestCar();
        Task<GenericResponceModel<bool>> UpdateAsync(int id, UpdateCarDto updateCarDto, CarCatagory carCatagory);
    }
}
