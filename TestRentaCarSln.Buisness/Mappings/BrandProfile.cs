using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Dtos.Brand;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.Buisness.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, GetBrandDto>().ReverseMap();
            CreateMap<Brand, PostBrandDto>().ReverseMap();
        }
    }
}
