using TestRentaCar.Buisness.Dtos.Customer;

namespace TestRentaCar.Buisness.Dtos.User
{
    public class CreateCustomerUserDto 
    {
        public CreateUserDto User { get; set; }
        public PostCustomerDto Customer { get; set; }
    }
}
