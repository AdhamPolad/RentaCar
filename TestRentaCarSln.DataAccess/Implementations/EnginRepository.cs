using TestRentaCarDataAccess.Abstractions;
using TestRentaCarDataAccess.Entities;
using TestRentaCarSln.DataAccess.Context;
using TestRentaCarSln.DataAccess.Implementations.Base;

namespace TestRentaCarDataAccess.Implementations
{
    public class EnginRepository : Repository<Engine>, IEnginRepository
    {
        public EnginRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }


    }
}
