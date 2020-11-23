using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;

namespace HiringGame.Application.Dtos
{
    public class TransactionForReturnDto : DtoBase
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public string Score { get; set; }
    }
}
