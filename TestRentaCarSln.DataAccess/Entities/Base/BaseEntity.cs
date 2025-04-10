namespace TestRentaCarSln.DataAccess.Entities.Base
{
    public class BaseEntity : IDeleteEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedDate { get ; set ; }
        public DateTime? UpdatedDate { get ; set ; }
        public DateTime CreatedDate { get; set; }
    }
}
