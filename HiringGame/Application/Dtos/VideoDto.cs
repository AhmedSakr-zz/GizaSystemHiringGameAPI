using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HiringGame.Application.Dtos
{
    public class VideoDto : DtoBase
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Video name is required.")]
        public string Name { get; set; }
        public string VirtualName { get; set; }
        public string PhysicalName { get; set; }
        [Required(ErrorMessage = "Video is required.")]
        public IFormFile VideoFile { get; set; }
    }
}
