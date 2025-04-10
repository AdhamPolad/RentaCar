using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.Review
{
    public class PostReviewDto
    {
        public int CarId { get; set; }
        public string Comment { get; set; }
        public ReviewRating ReviewRating { get; set; } 
    }
}
