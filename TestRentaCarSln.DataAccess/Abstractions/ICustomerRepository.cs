using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.DataAccess.Abstractions.Base;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.DataAccess.Abstractions
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByUserId(string userId);
    }
}
