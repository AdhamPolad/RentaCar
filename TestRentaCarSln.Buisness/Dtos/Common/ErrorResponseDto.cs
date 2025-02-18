using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRentaCarSln.Buisness.Dtos.Common
{
    public class ErrorResponseDto
    {
        public List<string> Errors { get; set; }

        public ErrorResponseDto(string error)
        {
            Errors.Add(error);
        }

        public ErrorResponseDto(List<string> errors)
        {
            Errors = errors;
        }

        public ErrorResponseDto()
        {
            
        }

    }
}
