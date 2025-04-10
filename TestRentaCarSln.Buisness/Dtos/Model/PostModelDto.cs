using TestRentaCar.Buisness.Dtos.Brand;
using TestRentaCarSln.Buisness.Dtos.Brand;

namespace TestRentaCar.Buisness.Dtos.Model
{
    public class PostModelDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public BrandDto BrandDto { get; set; }
    }
}
