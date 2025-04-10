using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.Payment
{
    public class UpdatePaymentDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }        

    }
}
