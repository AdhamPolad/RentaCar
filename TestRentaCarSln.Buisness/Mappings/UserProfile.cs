using AutoMapper;
using TestRentaCar.Buisness.Dtos.User;
using TestRentaCarDataAccess.Entities.Identity;

namespace TestRentaCar.Buisness.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, GetUserDto>().ReverseMap();
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
        }

    }
}
