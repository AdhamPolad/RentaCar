using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class Review : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }        
        public int Rating { get; set; }
        public string Comment { get; set; }

    }
}
