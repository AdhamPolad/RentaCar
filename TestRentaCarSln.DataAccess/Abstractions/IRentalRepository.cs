using System.Linq.Expressions;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarDataAccess.Abstractions
{
    public interface IRentalRepository : IRepository<Rental>
    {
        bool HasActiveRental(int customerId);
        Task<List<Rental>> GetActiveRentals();
        Task<PaginatedResult<IEnumerable<Rental>>> GetRentalsAsync(PaginationRequest paginationRequest, Expression<Func<Rental, bool>> filter);
        Task<Rental> GetRentalByIdAsync(int id);
        Task<bool> RentalExists(int customerId, int carId);
    }
}
