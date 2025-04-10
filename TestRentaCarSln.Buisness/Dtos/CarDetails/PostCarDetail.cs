using TestRentaCar.Buisness.Dtos.Engin;
using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Buisness.Dtos.CarDetails
{
    public class PostCarDetail
    {
        public PostEnginDto PostEnginDto { get; set; }
        public int Mileage { get; set; }  //yuruyus
        public int DoorsCount { get; set; }
        public string Color { get; set; }
        public Transmision Transmision { get; set; }  //Automatic or Mechanic


    }
}
