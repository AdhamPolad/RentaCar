using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class Discount : BaseEntity
    {
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DiscountCustomer> DiscountCustomers { get; set; }
    }
}

