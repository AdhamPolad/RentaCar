using TestRentaCar.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Car;

namespace TestRentaCar.Buisness.Dtos.Review
{
    public class GetReviewDto
    {
        public int Id { get; set; }     
        public string UserId { get; set; }
        public int CarId { get; set; }
        public CarDto CarDto { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
