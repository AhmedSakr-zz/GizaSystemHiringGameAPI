using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;
using HiringGame.Common;

namespace HiringGame.Application.Interfaces
{
    public interface IJobServices
    {
        IList<JobDto> GetList();
        ClientMessage<int> Create(JobDto model);
        ClientMessage<int> Update(JobDto model);
        ClientMessage<JobDto> GetById(int id);
         
    }
}
