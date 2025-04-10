using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCar.Buisness.Dtos.Discount
{
    public class PostDiscountDto
    {
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
    }
}
