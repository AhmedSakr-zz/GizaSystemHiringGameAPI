using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HiringGame.DataModel
{
    public class QuestionType
    {
        public QuestionType()
        {
            Questions = new List<Question>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Question> Questions { get; set; }
    }
}
