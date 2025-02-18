using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Abstractions;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Entities;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarSln.DataAccess.Implementations
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
      
    }
}
