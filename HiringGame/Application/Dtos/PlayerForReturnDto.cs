using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HiringGame.Application.Dtos
{
    public class PlayerForReturnDto : DtoBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string MobileNo { get; set; }

        public int jobId { get; set; }

        public string JobName { get; set; }
        public string Score { get; set; }
        public decimal TotalScore { get; set; }
        public string CvVirtualName { get; set; }
        public string CvPhysicalName { get; set; }

    }
}
