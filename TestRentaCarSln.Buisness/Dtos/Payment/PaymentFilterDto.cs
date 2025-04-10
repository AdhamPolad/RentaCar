namespace TestRentaCar.Buisness.Dtos.Payment
{
    public class PaymentFilterDto
    {
        public int? rentalId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
