using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions.Infrastructure;

namespace TestRentaCar.Api.Controllers.Identity
{
    [Route("api/v1/auths")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string usernameOrEmail, string password) =>
           Execute(await _authService.LoginAsync(usernameOrEmail, password));

        [Authorize(Roles = "User")]
        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken) =>
            Execute(await _authService.LoginWithRefreshTokenAsync(refreshToken));


        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> LogOut(string userNameOrEmail) =>
            Execute(await _authService.LogOut(userNameOrEmail));


        [HttpPost("password-reset")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PasswordReset(string email, string currentPas, string newPas) =>
            Execute(await _authService.PasswordResetAsync(email, currentPas, newPas));

    }
}
