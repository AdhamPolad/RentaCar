using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<PaginatedResult<IEnumerable<Maintenance>>> GetMaintenancesAsync(PaginationRequest paginationRequest, Expression<Func<Maintenance, bool>> filter)
        {
            var query = GetAll().Where(filter)
                .AsNoTracking();
                                
            int totalCount = await query.CountAsync();

            IEnumerable<Maintenance> maintenances = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();

            return new PaginatedResult<IEnumerable<Maintenance>>()
            {
                Data = maintenances,
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            };
        }

    }
}
