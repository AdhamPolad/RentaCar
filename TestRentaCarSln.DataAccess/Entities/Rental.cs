using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Rental : BaseEntity
    {
        public int CarId { get; set; }
        public Car? Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }    
        public decimal TotalPrice { get; set; }
        public decimal? DiscountAmount { get; set; } = 0;
        public decimal? PenaltyAmount { get; set; }
        public string? Status {  get; set; } 
        public Payment Payment { get; set; }
        public ICollection<Maintenance>? Maintenances { get; set; } // Bir Rental üçün bir neçə təmir ola bilər

    }
}
