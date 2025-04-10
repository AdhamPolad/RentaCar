using TestRentaCarDataAccess.Enums;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class CarDetails : BaseEntity
    {
        public Engine Engine { get; set; }
        public int EngineId { get; set; }
        public Transmision Transmision { get; set; }  //Automatic or Mechanic
        public int Mileage { get; set; }  //yuruyus
        public int DoorsCount { get; set; }       
        public string Color { get; set; }
        public Car Car { get; set; }
    }
}
