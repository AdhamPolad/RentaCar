using TestRentaCarSln.Buisness.Dtos.Brand;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCar.Buisness.Dtos.Model
{
    public class GetModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public GetBrandDto GetBrandDto { get; set; }
    }
}
