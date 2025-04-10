using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class CarCategory : BaseEntity
    {
        public string Name { get; set; } // "Sedan", "SUV", "İqtisadi", "Premium"
        public ICollection<Car> Cars { get; set; }
    }
}
