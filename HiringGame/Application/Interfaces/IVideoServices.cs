using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;
using HiringGame.Common;

namespace HiringGame.Application.Interfaces
{
    public interface IVideoServices
    {
        IList<VideoDto> GetList();
        ClientMessage<int> Create(VideoDto model);
        ClientMessage<int> Update(VideoDto model);
        ClientMessage<VideoDto> GetById(int id);
         
    }
}
