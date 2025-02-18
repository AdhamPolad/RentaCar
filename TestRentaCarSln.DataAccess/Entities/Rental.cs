using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Rental : BaseEntity
    {
        public int CarId { get; set; }
        public Car? Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }    
        public decimal TotalPrice { get; set; }
        public string? Status {  get; set; } 
        public Payment Payment { get; set; }

    }
}
