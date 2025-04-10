using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class Insurance : BaseEntity
    {
        public string PolicyName { get; set; }      // sığorta siyasətinin adı
        public decimal Price { get; set; }
        public Car Car { get; set; }
    }
}
