using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class QuestionDto : DtoBase
    {
        public QuestionDto()
        {
            Answers = new List<AnswerDto>();
        }
        public int? Id { get; set; }

        [Required(ErrorMessage = "Question title is required")]
        [Display(Name = "Question")]
        public string QuestionString { get; set; }

        public int? QuestionTypeId { get; set; }

        public IList<AnswerDto> Answers { get; set; }
    }
}
