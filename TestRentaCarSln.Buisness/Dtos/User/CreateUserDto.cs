namespace TestRentaCar.Buisness.Dtos.User
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Passwords { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
