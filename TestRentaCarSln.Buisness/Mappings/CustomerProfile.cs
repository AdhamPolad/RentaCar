using AutoMapper;
using TestRentaCar.Buisness.Dtos.Customer;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetCustomerDto>().ReverseMap();
            CreateMap<Customer, PostCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
        }

    }
}
