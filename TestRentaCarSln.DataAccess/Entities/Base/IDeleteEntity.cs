using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRentaCarDataAccess.Entities.Base;

namespace TestRentaCarSln.DataAccess.Entities.Base
{
    public interface IDeleteEntity : IUpdateEntity
    {
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedDate { get; set; }
    }
}
