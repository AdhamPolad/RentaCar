using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRentaCarSln.Buisness.Dtos.Brand
{
    public class GetBrandDto
    {
        public int Id { get; set; }         
        public string Name { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
