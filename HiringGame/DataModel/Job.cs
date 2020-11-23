using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.DataModel
{
    public class Job : AuditEntityBase
    {
        public Job()
        {
            Players = new List<Player>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Player> Players { get; set; }
    }
}
