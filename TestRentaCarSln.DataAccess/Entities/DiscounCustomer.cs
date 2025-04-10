using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities
{
    public class DiscountCustomer : BaseEntity
    {
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsUsed { get; set; }

    }
}
