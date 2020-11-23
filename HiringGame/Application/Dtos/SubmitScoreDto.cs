using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class SubmitScoreDto
    {
        [Required(ErrorMessage = "Player Id is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Total score is required.")]
        public string TotalScore { get; set; }
    }
}
