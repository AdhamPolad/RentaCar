using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestRentaCar.Buisness.Abstractions.Infrastructure;
using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Implementations.Infrastructure
{
    public class AuthService : IAuthService
    {// bu servis user i login edib token verir 
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<GenericResponceModel<TokenDto>> LoginAsync(string userNameOrEmail, string password)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);

            if (user == null)
            {
                return new GenericResponceModel<TokenDto>()
                {
                    Data = null,
                    StatusCode = 400,
                    Message = new List<string> { "User not found" }
                };
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                TokenDto tokenDto = await _tokenHandler.CreateAccessToken(user);
                await _userService.UpdateRefreshToken(tokenDto.RefreshToken, user, tokenDto.Expiration);
                return new GenericResponceModel<TokenDto>()
                {
                    Data = tokenDto,
                    StatusCode = 200,
                    Message = new List<string> { "User logged in" }
                };
            }
            else
                return new GenericResponceModel<TokenDto> { Data = null, StatusCode = 400, Message = new List<string> { "User not found" } };

        }

        public async Task<GenericResponceModel<TokenDto>> LoginWithRefreshTokenAsync(string refreshToken)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

            if (user != null && user?.RefreshTokenEndTime > DateTime.UtcNow)
            {
                TokenDto token = await _tokenHandler.CreateAccessToken(user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration.AddMinutes(10));
                return new GenericResponceModel<TokenDto>()
                {
                    Data = token,
                    StatusCode = 200,
                    Message = new List<string> { "User logged in" }
                };
            }
            else
            {
                return new GenericResponceModel<TokenDto>()
                {
                    Data = null,
                    StatusCode = 400,
                    Message = new List<string> { "User not logged in" }
                };
            }


        }

        public async Task<GenericResponceModel<bool>> LogOut(string userNameOrEmail)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);

            if (user == null)
            {
                return new GenericResponceModel<bool>()
                {
                    Data = false,
                    StatusCode = 400,
                    Message = new List<string> { "User not found" }
                };
            }

            user.RefreshTokenEndTime = null;
            user.RefreshToken = null;

            var result = await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();

            if (result.Succeeded)
            {
                return new GenericResponceModel<bool>()
                {
                    Data = true,
                    StatusCode = 200,
                    Message = new List<string> { "User logged out" }
                };
            }
            else
            {
                return new GenericResponceModel<bool>()
                {
                    Data = false,
                    StatusCode = 400,
                    Message = new List<string> { "User not logged out" }
                };
            }


        }

        public async Task<GenericResponceModel<bool>> PasswordResetAsync(string email, string currentPas, string newPas)
        {
            GenericResponceModel<bool> model = new()
            {
                Data = false,
                StatusCode = 400,
                Message = new List<string> { "User not found" }
            };

            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var data = await _userManager.ChangePasswordAsync(user, currentPas, newPas);

                if (data.Succeeded)
                {
                    model.Data = true;
                    model.StatusCode = 200;
                    return model;
                }
            }
            return model;

        }
    }
}

