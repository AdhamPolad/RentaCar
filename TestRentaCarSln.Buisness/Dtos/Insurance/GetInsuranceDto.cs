namespace TestRentaCar.Buisness.Dtos.Insurance
{
    public class GetInsuranceDto
    {
        public int Id { get; set; }                     
        public string PolicyName { get; set; }      // sığorta siyasətinin adı
        public decimal Price { get; set; }
    }
}
