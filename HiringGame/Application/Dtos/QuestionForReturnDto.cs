using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class QuestionForReturnDto
    {
        public QuestionForReturnDto()
        {
            Answers = new List<AnswerForReturnDto>();
        }
        public int? Id { get; set; }
        public string Question { get; set; }
        public IList<AnswerForReturnDto> Answers { get; set; }
    }
}
