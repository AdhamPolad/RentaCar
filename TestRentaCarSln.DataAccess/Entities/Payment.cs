using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Payment : BaseEntity
    {
        public int Rentalid { get; set; }
        public Rental Rental { get; set; }
        public decimal Amount { get; set; }     
        public DateTime PaymentDate { get; set; }   
        public string? Status { get; set; }
        public string PaymentMethod { get; set; }       
    }
}
