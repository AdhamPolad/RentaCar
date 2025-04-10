using System.Linq.Expressions;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.DataAccess.Abstractions
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<PaginatedResult<IEnumerable<Car>>> GetCarsAsync(PaginationRequest paginationRequest, Expression<Func<Car, bool>> filter);
        Task<Car> GetCarById(int id);
        Task<Car?> GetCheapestCar();
    }
}
