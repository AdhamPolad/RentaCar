using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.Review
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }         
        public string Comment { get; set; }
        public ReviewRating ReviewRating { get; set; }
    }
}
