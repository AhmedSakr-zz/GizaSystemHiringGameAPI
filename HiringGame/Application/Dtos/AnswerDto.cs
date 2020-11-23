using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class AnswerDto : DtoBase
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Answer is required")]
        public string AnswerString { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public int QuestionId { get; set; }
        public string PersonalityKey { get; set; }
    }
}
