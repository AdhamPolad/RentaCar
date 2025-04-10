using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarDataAccess.Entities.Identity;

namespace TestRentaCar.Buisness.Abstractions.Infrastructure
{
    public interface ITokenHandler
    {
        Task<TokenDto> CreateAccessToken(AppUser user);
        string CreateRefreshToken();
    }
}
