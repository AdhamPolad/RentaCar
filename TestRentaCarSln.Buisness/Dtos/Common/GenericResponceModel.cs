using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRentaCarSln.Buisness.Dtos.Common
{
    public class GenericResponceModel<T>
    {
        public T Data { get; set; } 
        public int StatusCode { get; set; } 
    }
}
