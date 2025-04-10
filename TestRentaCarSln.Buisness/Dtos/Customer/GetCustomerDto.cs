using TestRentaCarDataAccess.Entities.Identity;

namespace TestRentaCar.Buisness.Dtos.Customer
{
    public class GetCustomerDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DriverLisenceNumber { get; set; }
    }
}
