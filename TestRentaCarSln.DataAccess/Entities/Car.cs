using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Car : BaseEntity
    {
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAviable { get; set; }         
        public ICollection<Rental> Rental { get; set; }



    }
}
