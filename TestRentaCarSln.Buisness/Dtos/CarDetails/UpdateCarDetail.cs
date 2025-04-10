using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.CarDetails
{
    public class UpdateCarDetail
    {
        public int Id { get; set; }
        public int EngineId { get; set; }
        public Transmision Transmision { get; set; }  //Automatic or Mechanic
        public int Mileage { get; set; }  //yuruyus
        public int DoorsCount { get; set; }
        public string Color { get; set; }
    }
}
