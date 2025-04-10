using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarDataAccess.Enums;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Buisness.Abstractions.Infrastructure
{
    public interface IUserService
    {
        Task<GenericResponceModel<bool>> UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate);
        Task<GenericResponceModel<bool>> AssignRoleToUserAsync(string userId, string[] roles);
        Task<GenericResponceModel<CreateUserResponceDto>> CreateAsync(CreateUserDto createUserDto);
        Task<GenericResponceModel<List<GetUserDto>>> GetAllAsync();
        Task<GenericResponceModel<string[]>> GetRolesToUserAsync(string userIdOrName);
        Task<GenericResponceModel<bool>> DeleteUserAsync(string userIdOrName);
        Task<GenericResponceModel<bool>> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<GenericResponceModel<CreateUserResponceDto>> RegisterCustomer(CreateCustomerUserDto createCustomerUserDto, Roles role);

    }
}
