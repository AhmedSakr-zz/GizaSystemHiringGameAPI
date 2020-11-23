using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class ForgetPasswordDto
    {
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
}
