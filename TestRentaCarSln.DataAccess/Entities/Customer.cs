using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Entities.Identity;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }       
        public string FullName { get; set; }    
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DriverLisenceNumber { get; set; }
        public ICollection<Rental> Rental { get; set; }
        public ICollection<DiscountCustomer> DiscountCustomers { get; set; }

    }
}
