namespace TestRentaCarDataAccess.Model
{
    public class PaginatedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public T Data { get; set; }
    }
}
