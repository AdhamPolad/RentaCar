using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCarSln.Buisness.Abstractions
{
    public interface ICarService
    {
        Task<GenericResponceModel<IEnumerable<GetCarDto>>> GetAllAsync();
        Task<GenericResponceModel<GetCarDto>> GetByIdAsync(int id);
        Task<GenericResponceModel<PostCarDto>> CreateAsync(PostCarDto postCarDto);
        Task<GenericResponceModel<bool>> DeleteAsync(int id);

    }
}
