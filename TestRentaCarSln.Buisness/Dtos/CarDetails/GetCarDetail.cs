using TestRentaCar.Buisness.Dtos.Engin;

namespace TestRentaCar.Buisness.Dtos.CarDetails
{
    public class GetCarDetail
    {
        public int Id { get; set; }
        public GetEnginDto GetEnginDto { get; set; }
        public int Mileage { get; set; }  //yuruyus
        public int DoorsCount { get; set; }
        public string Color { get; set; }
    }
}
