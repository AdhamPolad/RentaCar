using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities
{
    public class Car : BaseEntity
    {
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }   
        public string LicensePlate { get; set; }  // Avtomobilin nömrə nişanı
        public CarDetails? CarDetails { get; set; }
        public int? CarDetailId { get; set; }
        public CarCategory CarCatagory { get; set; }
        public int CarCatagoryId { get; set; }
        public Branch Branch{ get; set; }    //filial
        public int BranchId{ get; set; }   
        public Insurance Insurance { get; set; }
        public int InsuranceId { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Review> Reviews { get; set; }    

    }
}
