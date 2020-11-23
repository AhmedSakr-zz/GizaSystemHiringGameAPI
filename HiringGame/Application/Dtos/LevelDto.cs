using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class LevelDto : DtoBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Total questions is required")]
        [Range(0,int.MaxValue,ErrorMessage = "Invalid total questions value")]
        public int TotalQuestions { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
    }
}
