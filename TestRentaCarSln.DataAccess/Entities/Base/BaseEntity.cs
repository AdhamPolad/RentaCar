using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRentaCarSln.DataAccess.Entities.Base
{
    public class BaseEntity : IDeleteEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTime DeletedDate { get ; set ; }
        public int DeleteUserId { get ; set ; }
    }
}
