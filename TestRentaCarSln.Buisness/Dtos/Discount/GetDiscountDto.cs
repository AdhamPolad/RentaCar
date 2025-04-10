namespace TestRentaCar.Buisness.Dtos.Discount
{
    public class GetDiscountDto
    {

        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<GetDiscountCustomer> GetDiscountCustomers { get; set; }
    }
}
