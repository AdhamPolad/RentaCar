using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarSln.DataAccess.Implementations
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly AppDbContext _appDbContext;
        public CarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Car?> GetCheapestCar()
        {
            return await _appDbContext.Cars.OrderBy(x => x.PricePerDay)
                                            .Include(x => x.Model)
                                            .ThenInclude(x => x.Brand)
                                            .Include(x => x.CarCatagory)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<IEnumerable<Car>>> GetCarsAsync(PaginationRequest paginationRequest, Expression<Func<Car, bool>> filter)
        {
            var query = GetAll().Where(filter)
                          .Include(x => x.Model)
                          .ThenInclude(x => x.Brand)
                          .Include(x => x.CarCatagory)
                          .Include(x=>x.CarDetails)
                          .ThenInclude(x=>x.Engine)
                          .AsNoTracking();

            int totalCount = await query.CountAsync();

            IEnumerable<Car> cars = await query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                                               .Take(paginationRequest.PageSize)
                                               .ToListAsync();

            return new PaginatedResult<IEnumerable<Car>>()
            {
                TotalCount = totalCount,
                PageNum = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                Data = cars
            };

        }

        public async Task<Car> GetCarById(int id)
        {
            Car? car = await _appDbContext.Cars.Where(x => x.Id == id)
                                              .Include(x => x.Model)
                                              .ThenInclude(x => x.Brand)
                                              .Include(x => x.CarCatagory)
                                              .Include(x=>x.CarDetails)
                                              .ThenInclude(x=>x.Engine)
                                              .FirstOrDefaultAsync();

            return car;
        }
      
    }
}
