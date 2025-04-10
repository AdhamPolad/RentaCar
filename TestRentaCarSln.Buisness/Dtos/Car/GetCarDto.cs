using TestRentaCar.Buisness.Dtos.CarCatagory;
using TestRentaCar.Buisness.Dtos.CarDetails;
using TestRentaCar.Buisness.Dtos.Engin;
using TestRentaCar.Buisness.Dtos.Model;

namespace TestRentaCarSln.Buisness.Dtos.Car
{
    public class GetCarDto
    {
        public int Id { get; set; }
        public GetModelDto GetModelDto { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string LicensePlate { get; set; }  // Avtomobilin nömrə nişanı
        public GetCarDetail GetCarDetail { get; set; }
        public GetCarCatagory GetCarCatagory { get; set; }
    }
}
