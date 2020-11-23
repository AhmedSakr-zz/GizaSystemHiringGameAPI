using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.DataModel
{
    public class Question : AuditEntityBase
    {
        public Question()
        {
            Answers = new List<Answer>();
            Transactions=new List<Transaction>();
        }
        public int Id { get; set; }
        public string QuestionString { get; set; }

        public int QuestionTypeId { get; set; }

        [ForeignKey(nameof(QuestionTypeId))]
        public QuestionType QuestionType { get; set; }

        public IList<Answer> Answers { get; set; }
        public IList<Transaction> Transactions { get; set; }

    }
}
