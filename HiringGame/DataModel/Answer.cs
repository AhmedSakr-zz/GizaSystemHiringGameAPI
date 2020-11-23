using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.DataModel
{
    public class Answer : AuditEntityBase
    {
        public Answer()
        {
            Transactions = new List<Transaction>();
        }
        public int Id { get; set; }
        public string AnswerString { get; set; }
        public bool? IsCorrectAnswer { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public string PersonalityKey { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}
