using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringGame.Application.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [Display(Name = "New Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm New Password")]
        public string ConfirmPassword { get; set; }
    }
}
