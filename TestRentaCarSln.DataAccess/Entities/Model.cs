using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Car> Cars { get; set; }  

    }
}
