using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class Level3Transaction
    {
        public int  PlayerId { get; set; }
        public int QuestionId { get; set; }
        public List<TransactionDto> AnswersList { get; set; }
    }
}
