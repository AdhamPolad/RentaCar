using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.Insurance
{
    public class PostInsuranceDto
    {
        public decimal Price { get; set; }
        public CarInsuranceType CarInsuranceType { get; set; }
    }
}
