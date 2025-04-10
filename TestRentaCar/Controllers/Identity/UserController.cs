using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions.Infrastructure;
using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Api.Controllers.Identity
{
    [Route("api/v1/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-customer")]
        public async Task<IActionResult> RegisterCustomer(CreateCustomerUserDto createCustomerUserDto) =>
            Execute(await _userService.RegisterCustomer(createCustomerUserDto, Roles.Customer));

        [HttpGet]
        public async Task<IActionResult> All() =>
            Execute(await _userService.GetAllAsync());

        [HttpPost("register-user")]
        public async Task<IActionResult> Register(CreateUserDto createUserDto) =>
            Execute(await _userService.CreateAsync(createUserDto));

        [HttpGet("get-roles-to-users")]
        public async Task<IActionResult> GetRolesToUser(string userIdOrName) =>
            Execute(await _userService.GetRolesToUserAsync(userIdOrName));

        [HttpDelete]
        public async Task<IActionResult> Delete(string userIdOrName) =>
            Execute(await _userService.DeleteUserAsync(userIdOrName));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto) =>
            Execute(await _userService.UpdateUserAsync(updateUserDto));

        [HttpPost("assign-role-to-user")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string[] roles) =>
            Execute(await _userService.AssignRoleToUserAsync(userId, roles));

        [HttpPut("update-refresh-token")]
        public async Task<IActionResult> UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate) =>
            Execute(await _userService.UpdateRefreshToken(refreshToken, user, accessTokenDate));

    }
}
