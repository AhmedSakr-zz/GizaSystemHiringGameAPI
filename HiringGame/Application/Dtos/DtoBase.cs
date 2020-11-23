using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class DtoBase
    {
        public bool IsActive { get; set; }
        public int? CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
