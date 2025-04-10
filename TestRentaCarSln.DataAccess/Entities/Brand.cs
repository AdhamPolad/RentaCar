using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
