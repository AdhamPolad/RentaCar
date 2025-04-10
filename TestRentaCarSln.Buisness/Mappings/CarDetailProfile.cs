using AutoMapper;
using TestRentaCar.Buisness.Dtos.CarDetails;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.Buisness.Dtos.Car;

namespace TestRentaCar.Buisness.Mappings
{
    public class CarDetailProfile : Profile
    {
        public CarDetailProfile()
        {
            CreateMap<PostCarDetail, CarDetails>()
           /*.ForMember(dest => dest.Transmision, opt => opt.MapFrom(src => src.Transmision.ToString()))*/.ReverseMap();

            CreateMap<GetCarDetail, CarDetails>()
                .ForMember(dest => dest.Engine, opt => opt.MapFrom(src => src.GetEnginDto))
                .ReverseMap();

            CreateMap<UpdateCarDetail, CarDetails>().ReverseMap();
        }
    }
}
