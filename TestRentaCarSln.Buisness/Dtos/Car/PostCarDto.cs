using TestRentaCar.Buisness.Dtos.CarDetails;
using TestRentaCarDataAccess.Enums;

namespace TestRentaCarSln.Buisness.Dtos.Car
{
    public class PostCarDto
    {
        public int ModelId { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public CarCatagory CarCatagory { get; set; }
        public string LicensePlate { get; set; }  // Avtomobilin nömrə nişanı
        public PostCarDetail CarDetails { get; set; }
        public int BranchId { get; set; }
        public int InsuranceId { get; set; }
    }
}
