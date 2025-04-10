using TestRentaCar.Buisness.Dtos.Maintenance;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IMaintenanceService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetMaintenanceDto>>>> GetAllAsync(PaginationRequest paginationRequest, int? rentalId, DateTime minMaintentanceDate, DateTime maxMaintenanceDate);
        Task<GenericResponceModel<GetMaintenanceDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<bool>> CreateAsync(PostMaintenanceDto postMaintenanceDto);
        Task<GenericResponceModel<bool>> UpdateAsync(UpdateMaintenanceDto updateMaintenanceDto);
        Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType, int id);
    }
}
