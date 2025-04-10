using TestRentaCarSln.DataAccess.Entities.Base;

namespace TestRentaCarDataAccess.Entities.Base
{
    public interface IUpdateEntity : ICreateEntity
    {
        public DateTime? UpdatedDate { get; set; }
    }
}
