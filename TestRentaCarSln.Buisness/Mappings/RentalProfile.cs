using AutoMapper;
using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<GetRentalDto, Rental>().ReverseMap();
        }

    }
}
