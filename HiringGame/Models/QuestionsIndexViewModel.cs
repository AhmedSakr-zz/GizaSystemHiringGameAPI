using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;

namespace HiringGame.Models
{
    public class QuestionsIndexViewModel
    {
        public QuestionsIndexViewModel()
        {
            Questions=new List<QuestionDto>();
        }

        public int TypeId { get; set; }

        public IList<QuestionDto> Questions { get; set; }
    }
}
