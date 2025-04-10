using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Car> Cars { get; set; }
    }
}
