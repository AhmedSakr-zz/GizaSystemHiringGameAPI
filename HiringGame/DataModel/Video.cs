using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiringGame.DataModel
{
    public class Video : AuditEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VirtualName { get; set; }
        public string PhysicalName { get; set; }
    }
}
