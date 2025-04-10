using AutoMapper;
using TestRentaCar.Buisness.Dtos.Insurance;
using TestRentaCarDataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<Insurance, PostInsuranceDto>().ReverseMap();
            CreateMap<Insurance, GetInsuranceDto>().ReverseMap();
            CreateMap<Insurance, UpdateInsuranceDto>().ReverseMap();
        }

    }
}
