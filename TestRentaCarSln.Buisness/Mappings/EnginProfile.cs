using AutoMapper;
using TestRentaCar.Buisness.Dtos.Engin;
using TestRentaCarDataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class EnginProfile : Profile
    {
        public EnginProfile()
        {
            CreateMap<Engine, GetEnginDto>().ReverseMap();
            CreateMap<Engine, PostEnginDto>().ReverseMap();
        }

    }
}
