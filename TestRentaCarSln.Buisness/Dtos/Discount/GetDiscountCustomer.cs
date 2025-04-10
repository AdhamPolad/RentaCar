using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCar.Buisness.Dtos.Discount
{
    public class GetDiscountCustomer
    {
        public int DiscountId { get; set; }
        public int CustomerId { get; set; }
        public bool IsUsed { get; set; }

    }
}
