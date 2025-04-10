using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Payment : BaseEntity
    {
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public decimal Amount { get; set; }     
        public DateTime PaymentDate { get; set; }   
        public string? Status { get; set; }  //pending, completed, failed etc..
        public string PaymentMethod { get; set; }       
    }
}
