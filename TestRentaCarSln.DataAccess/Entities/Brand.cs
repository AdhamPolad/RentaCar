using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string Model { get; set; }  
        public int Year { get; set; }       
        public ICollection<Car> Car { get; set; }
    }
}
