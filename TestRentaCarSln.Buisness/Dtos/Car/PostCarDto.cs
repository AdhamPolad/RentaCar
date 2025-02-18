﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarSln.Buisness.Dtos.Brand;
using TestRentaCarSln.DataAccess.Entities;

namespace TestRentaCarSln.Buisness.Dtos.Car
{
    public class PostCarDto
    {
        public PostBrandDto Brand { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAviable { get; set; }
    }
}
