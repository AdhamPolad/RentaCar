using AutoMapper;
using TestRentaCar.Buisness.Dtos.CarCatagory;
using TestRentaCarDataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class CarCatagoryProfile : Profile
    {
        public CarCatagoryProfile()
        {
            CreateMap<CarCategory, GetCarCatagory>().ReverseMap();
            CreateMap<CarCategory, PostCarCatagory>().ReverseMap();
        }

    }
}
