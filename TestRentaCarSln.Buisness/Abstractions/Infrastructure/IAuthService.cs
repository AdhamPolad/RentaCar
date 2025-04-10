using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions.Infrastructure
{
    public interface IAuthService
    {
        Task<GenericResponceModel<TokenDto>> LoginAsync(string userNameOrEmail, string password);
        Task<GenericResponceModel<TokenDto>> LoginWithRefreshTokenAsync(string refreshToken);
        Task<GenericResponceModel<bool>> LogOut(string userNameOrEmail);
        Task<GenericResponceModel<bool>> PasswordResetAsync(string email, string currentPas, string newPas);

    }
}
