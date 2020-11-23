using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class AnswerForReturnDto
    {
        public int? Id { get; set; }
        public string Answer { get; set; }
        public string IsCorrect { get; set; }
        public string PersonalityKey { get; set; }
    }
}
