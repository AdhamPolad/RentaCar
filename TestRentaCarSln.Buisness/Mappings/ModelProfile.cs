using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestRentaCar.Buisness.Dtos.Model;
using TestRentaCarDataAccess.Entities;
using Model = TestRentaCarDataAccess.Entities.Model;

namespace TestRentaCar.Buisness.Mappings
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Model, PostModelDto>()
           .ReverseMap();
            CreateMap<Model, GetModelDto>()
           .ForMember(dest => dest.GetBrandDto, opt => opt.MapFrom(src => src.Brand)) // BrandId map et
           .ReverseMap();
            CreateMap<Model, UpdateModelDto>().ReverseMap();
        }

    }
}
