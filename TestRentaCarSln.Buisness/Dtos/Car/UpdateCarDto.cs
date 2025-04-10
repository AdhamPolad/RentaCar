using TestRentaCar.Buisness.Dtos.CarDetails;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.Car
{
    public class UpdateCarDto
    {
        public int ModelId { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string LicensePlate { get; set; }  // Avtomobilin nömrə nişanı
        public UpdateCarDetail UpdateCarDetail { get; set; }
        public int BranchId { get; set; }
        public int InsuranceId { get; set; }

    }
}
