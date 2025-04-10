using AutoMapper;
using TestRentaCar.Buisness.Dtos.Discount;
using TestRentaCarDataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, GetDiscountDto>().ReverseMap();
        }

    }
}
