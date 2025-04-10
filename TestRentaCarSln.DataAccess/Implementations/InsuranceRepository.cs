using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class InsuranceRepository : Repository<Insurance>, IInsuranceRepository
    {
        private readonly AppDbContext _appDbContext;
        public InsuranceRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedResult<IEnumerable<Insurance>>> GetInsurances(PaginationRequest paginationRequest, Expression<Func<Insurance, bool>> filter)
        {
            var query = GetAll().Where(filter)
                                .AsNoTracking();

            int totalCount = await query.CountAsync();

            IEnumerable<Insurance> insurances = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                                           .Take(paginationRequest.PageSize)
                                                           .ToListAsync();

            return new PaginatedResult<IEnumerable<Insurance>>()
            {
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                Data = insurances
            };

        }

    }
}
