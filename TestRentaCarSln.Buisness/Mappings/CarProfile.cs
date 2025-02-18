using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Dtos.Car;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.Buisness.Mappings
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, GetCarDto>().ReverseMap();
            CreateMap<Car, PostCarDto>().ReverseMap();
        }
    }
}
