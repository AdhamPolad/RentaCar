using AutoMapper;
using TestRentaCar.Buisness.Dtos.Payment;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, GetPaymentDto>().ReverseMap();
        }

    }
}
