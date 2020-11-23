using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.DataModel
{
    public class Player:AuditEntityBase
    {
        public Player()
        {
            Transactions=new List<Transaction>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string CvVirtualName { get; set; }
        public string CvPhysicalName { get; set; }
        public int JobId { get; set; }
        public string Score { get; set; }

        [ForeignKey("JobId")]
        public Job Job { get; set; }
        public IList<Transaction> Transactions { get; set; }
        
    }
}
