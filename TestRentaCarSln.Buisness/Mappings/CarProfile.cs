using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCar.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.Buisness.Mappings
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<PostCarDto, Car>()
            //.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.PostModelDto))
            //.ForMember(dest => dest.Engin, opt => opt.MapFrom(src => src.PostEnginDto))
            //.ForMember(dest => dest.CarCatagory, opt => opt.MapFrom(src => src.PostCarCatagory))
            //.ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            //.ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.InsuranceId))
            .ReverseMap();

            CreateMap<UpdateCarDto, Car>().ReverseMap();

            CreateMap<GetCarDto, Car>()
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.GetModelDto))
            .ForMember(dest => dest.CarCatagory, opt => opt.MapFrom(src => src.GetCarCatagory))
            .ForMember(dest => dest.CarDetails, opt => opt.MapFrom(src => src.GetCarDetail))
            .ReverseMap();

            CreateMap<CarDto, Car>()
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.GetModelDto))
            .ForMember(dest => dest.CarCatagory, opt => opt.MapFrom(src => src.GetCarCatagory))
            .ReverseMap();
        }
    }
}
