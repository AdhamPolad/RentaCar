namespace TestRentaCar.Buisness.Dtos.Discount
{
    public class UpdateDiscountDto
    {
        public int Id { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
