using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class Engine : BaseEntity
    {
        public string EnginType { get; set; }  //benzin or diesel?
        public decimal EnginCapacity { get; set; } // 4.4, 2.0
        public ICollection<CarDetails> CarDetails { get; set; }
    }
}
