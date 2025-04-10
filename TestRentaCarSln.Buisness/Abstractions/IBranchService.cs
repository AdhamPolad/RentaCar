using TestRentaCar.Buisness.Dtos.Branch;
using TestRentaCar.Buisness.Dtos.Brand;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Brand;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions
{
    public interface IBranchService
    {
        Task<GenericResponceModel<PaginatedResult<IEnumerable<GetBranchDto>>>> GetAllAsync(PaginationRequest paginationRequest);
        Task<GenericResponceModel<GetBranchDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<PostBranchDto>> CreateAsync(PostBranchDto postBranchDto);
        Task<GenericResponceModel<bool>> DeleteAsync(DeleteType deleteType, int id);
        Task<GenericResponceModel<bool>> UpdateAsync(UpdateBranchDto updateBranchDto);
    }
}
