using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.Insurance
{
    public class UpdateInsuranceDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public CarInsuranceType CarInsuranceType { get; set; }
    }
}
