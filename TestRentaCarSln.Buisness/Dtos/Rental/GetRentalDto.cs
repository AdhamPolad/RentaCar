using TestRentaCar.Buisness.Dtos.Customer;
using TestRentaCar.Buisness.Dtos.Payment;

namespace TestRentaCar.Buisness.Dtos.Rental
{
    public class GetRentalDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public GetCustomerDto Customer { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public GetPaymentDto Payment { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? PenaltyAmount { get; set; }
    }
}
