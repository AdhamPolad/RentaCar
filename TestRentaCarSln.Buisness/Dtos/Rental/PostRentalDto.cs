using TestRentaCarDataAccess.Enums;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Dtos.Rental
{
    public class PostRentalDto
    {
        public int CarId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
