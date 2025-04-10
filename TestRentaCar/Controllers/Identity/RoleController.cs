using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions.Infrastructure;

namespace TestRentaCar.Api.Controllers.Identity
{
    [Route("api/v1/roles")]
    [Authorize(Roles = "Admin")]    
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Execute(await _roleService.GetAllRoles());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Execute(await _roleService.GetRoleById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            return Execute(await _roleService.CreateRole(roleName));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Execute(await _roleService.DeleteRole(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, string name)
        {
            return Execute(await _roleService.UpdateRole(id, name));
        }

    }
}
