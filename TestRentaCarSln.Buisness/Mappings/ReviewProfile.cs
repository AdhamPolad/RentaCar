using AutoMapper;
using TestRentaCar.Buisness.Dtos.Review;
using TestRentaCarDataAccess.Entities;

namespace TestRentaCar.Buisness.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, GetReviewDto>()
                .ForMember(dest => dest.CarDto, opt => opt.MapFrom(src => src.Car))
                .ReverseMap();
        }

    }
}
