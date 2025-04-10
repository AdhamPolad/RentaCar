using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions.Infrastructure
{
    public interface IRoleService
    {
        Task<GenericResponceModel<IEnumerable<AppRole>>> GetAllRoles();
        Task<GenericResponceModel<AppRole>> GetRoleById(string id);
        Task<GenericResponceModel<bool>> CreateRole(string name);
        Task<GenericResponceModel<bool>> DeleteRole(string id);
        Task<GenericResponceModel<bool>> UpdateRole(string id, string name);

    }
}
