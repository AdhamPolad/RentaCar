using Microsoft.AspNetCore.Identity;

namespace TestRentaCarDataAccess.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndTime { get; set; }
        public ICollection<Review> Reviews { get; set; }    
    }
}
