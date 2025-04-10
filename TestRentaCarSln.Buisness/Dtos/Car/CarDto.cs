using TestRentaCar.Buisness.Dtos.CarCatagory;
using TestRentaCar.Buisness.Dtos.Engin;
using TestRentaCar.Buisness.Dtos.Model;

namespace TestRentaCar.Buisness.Dtos.Car
{
    public class CarDto
    {
        public int Id { get; set; }
        public GetModelDto GetModelDto { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string LicensePlate { get; set; }  // Avtomobilin nömrə nişanı
        public GetCarCatagory GetCarCatagory { get; set; }
    }
}
