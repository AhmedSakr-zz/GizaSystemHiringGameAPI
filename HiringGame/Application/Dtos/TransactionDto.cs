using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringGame.DataModel;

namespace HiringGame.Application.Dtos
{
    public class TransactionDto
    {
        public int PlayerId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public AppEnum.PersonalityChoices? PersonalityAnswerChoiceId { get; set; }
        public string Score { get; set; }
    }
}
