using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringGame.DataModel;

namespace HiringGame.Application.Dtos
{
    public class JobDto : DtoBase
    {
        
        public int? Id { get; set; }
        [Required(ErrorMessage = "Job name is required.")]
        public string Name { get; set; }
         
    }
}
