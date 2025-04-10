using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestRentaCar.Buisness.Abstractions.Infrastructure;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Implementations.Infrastructure
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GenericResponceModel<bool>> CreateRole(string name)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            AppRole appRole = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            };

            IdentityResult identityResult = await _roleManager.CreateAsync(appRole);
            if (identityResult.Succeeded)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Role created successfully");
                return model;
            }

            return model;
        }

        public async Task<GenericResponceModel<bool>> DeleteRole(string id)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            AppRole appRole = await _roleManager.FindByIdAsync(id);

            if(appRole is null)
            {
                return model;
            }

            IdentityResult result = await _roleManager.DeleteAsync(appRole);

            if (result.Succeeded)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Role deleted successfully");
            }

            model.Message.Add("Role not deleted");
            return model;

        }

        public async Task<GenericResponceModel<IEnumerable<AppRole>>> GetAllRoles()
        {
            GenericResponceModel<IEnumerable<AppRole>> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            var roles = await _roleManager.Roles.ToListAsync();

            if (roles.Any())
            {
                model.Data = roles;
                model.StatusCode = 200;
                model.Message.Add("Roles found");
                return model;
            }

            model.Message.Add("Roles not found");
            return model;
        }

        public async Task<GenericResponceModel<AppRole>> GetRoleById(string id)
        {
            GenericResponceModel<AppRole> model = new()
            {
                Data = null,
                StatusCode = 404,
                Message = new List<string>()
            };

            AppRole appRole = await _roleManager.FindByIdAsync(id);

            if(appRole is not null)
            {
                model.Data = appRole;
                model.StatusCode = 200;
                model.Message.Add("Role found");
                return model;
            }

            model.Message.Add("Role not found");
            return model;
        }

        public async Task<GenericResponceModel<bool>> UpdateRole(string id, string name)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string>()
            };

            AppRole appRole = await _roleManager.FindByIdAsync(id);

            if(appRole is null)
            {
                model.Message.Add("Role not found");
                return model;
            }

            appRole.Name = name;
            IdentityResult identityResult = await _roleManager.UpdateAsync(appRole);

            if (identityResult.Succeeded)
            {
                model.Data = true;
                model.StatusCode = 200;
                model.Message.Add("Role updated successfully");
                return model;
            }

            model.Message.Add("Role not updated");
            return model;
        }
    }
}
