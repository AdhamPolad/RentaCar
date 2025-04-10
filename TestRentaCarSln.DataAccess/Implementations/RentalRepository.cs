using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        private readonly AppDbContext _appDbContext;
        public RentalRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> RentalExists(int customerId, int carId)
        {
            return await GetAll().AnyAsync(x => x.CustomerId == customerId && x.CarId == carId);
        }

        public async Task<List<Rental>> GetActiveRentals()
        {
            return await GetAll().Where(x => x.Status == RentalStatus.Active.ToString()).ToListAsync();
        }

        public bool HasActiveRental(int customerId)
        {
            return GetAll().Any(x => x.CustomerId == customerId && x.Status == RentalStatus.Active.ToString());
        }

        public async Task<Rental> GetRentalByIdAsync(int id)
        {
            var rental = await GetAll().Where(x => x.Id == id)
                                        .Include(x => x.Payment)
                                        .Include(x => x.Customer)
                                        .FirstOrDefaultAsync();

            return rental;
        }


        public async Task<PaginatedResult<IEnumerable<Rental>>> GetRentalsAsync(PaginationRequest paginationRequest, Expression<Func<Rental, bool>> filter)
        {
            var query = GetAll().Where(filter)
                                .Include(x => x.Customer)
                                .Include(x => x.Payment)
                                .AsNoTracking();

            int totalCount = await query.CountAsync();

            var rentals = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                               .Take(paginationRequest.PageSize)
                               .ToListAsync();

            return new PaginatedResult<IEnumerable<Rental>>
            {
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                Data = rentals
            };

        }
    }
}
