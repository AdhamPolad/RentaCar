namespace TestRentaCar.Buisness.Dtos.Payment
{
    public class GetPaymentDto
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Status { get; set; }  //pending, completed, failed etc..
        public string PaymentMethod { get; set; }
    }
}
