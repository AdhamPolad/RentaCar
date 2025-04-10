using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class Maintenance : BaseEntity
    {
        public int RentalId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? InsuranceCoverage { get; set; } // Sığorta tərəfindən qarşılanan məbləğ
        public Rental Rental { get; set; }
    }
}
