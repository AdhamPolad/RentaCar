using AutoMapper;
using TestRentaCar.Buisness.Dtos.Branch;
using TestRentaCarDataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class BranchProfile : Profile
    {
        public BranchProfile()
        {
            CreateMap<Branch, GetBranchDto>().ReverseMap();
        }
    }
}
