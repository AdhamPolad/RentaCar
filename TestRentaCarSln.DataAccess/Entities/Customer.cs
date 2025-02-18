using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }    
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DriverLisenceNumber { get; set; }
        public ICollection<Rental> Rental { get; set; }

    }
}
