using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HiringGame.Application.Dtos
{
    public class PlayerDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [MaxLength(50)]
        public string MobileNo { get; set; }

        public IFormFile PlayerCV { get; set; }
        [Required]
        public int jobId { get; set; }

        
    }
}
