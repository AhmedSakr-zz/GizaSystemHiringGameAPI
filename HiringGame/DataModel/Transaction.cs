using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HiringGame.DataModel
{
    public class Transaction : AuditEntityBase
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public int? AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }
        public int? PersonalityAnswerChoiceId { get; set; }
        public PersonalityAnswerChoice PersonalityAnswerChoice { get; set; }
        public string Score { get; set; }
    }
}
