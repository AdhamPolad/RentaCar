namespace TestRentaCar.Buisness.Dtos.Rental
{
    public class UpdateRentalDto
    {
        public int Id { get; set; } 
        public int CarId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
