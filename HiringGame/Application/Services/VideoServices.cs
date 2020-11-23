using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using HiringGame.Common;
using HiringGame.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HiringGame.Application.Services
{
    public class VideoServices : IVideoServices
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public VideoServices(AppDbContext dbContext,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public IList<VideoDto> GetList()
        {
            var allVideos = _dbContext.Videos.AsQueryable().ToList();
            return _mapper.Map<List<VideoDto>>(allVideos);
        }

        public ClientMessage<int> Create(VideoDto model)
        {
            var clientMessage = new ClientMessage<int>();

            try
            {
                var video = _mapper.Map<Video>(model);
                video.VirtualName = model.VideoFile.FileName;
                video.PhysicalName = DateTime.Now.ToFileTime().ToString() +
                                     Path.GetExtension(model.VideoFile.FileName);
                var path = _webHostEnvironment.WebRootPath + "/App_Files/" + video.PhysicalName;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.VideoFile.CopyTo(fileStream);
                }

                _dbContext.Videos.Add(video);
                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Video created successfully" };
                clientMessage.ReturnedData = video.Id;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;

            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message };
                clientMessage.ReturnedData = 0;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }

        }

        public ClientMessage<VideoDto> GetById(int id)
        {
            var clientMessage = new ClientMessage<VideoDto>();


            var video = _dbContext.Videos.FirstOrDefault(t => t.Id == id);

            if (video == null)
            {
                clientMessage = new ClientMessage<VideoDto>
                {
                    ClientStatusCode = DataEnum.OperationStatus.Error,
                    ClientMessageContent = new List<string> { "Id is not correct" }
                };
                return clientMessage;
            }


            clientMessage.ReturnedData = _mapper.Map<VideoDto>(video);
            clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
            return clientMessage;



        }

        public ClientMessage<int> Update(VideoDto model)
        {
            var clientMessage = new ClientMessage<int>();

            try
            {
                var video = _dbContext.Videos.Find(model.Id);
                video.Name = model.Name;
                video.IsActive = model.IsActive;

                if (model.VideoFile != null)
                {
                    video.VirtualName = model.VideoFile.FileName;
                    video.PhysicalName = DateTime.Now.ToFileTime().ToString() +
                                         Path.GetExtension(model.VideoFile.FileName);
                    var path = _webHostEnvironment.WebRootPath + "/App_Files/" + video.PhysicalName;

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        model.VideoFile.CopyTo(fileStream);
                    }
                }
                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Video updated successfully" };
                clientMessage.ReturnedData = video.Id;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
                return clientMessage;
            }
            catch (Exception ex)
            {
                clientMessage.ClientMessageContent = new List<string> { ex.Message };
                clientMessage.ReturnedData = 0;
                clientMessage.ClientStatusCode = DataEnum.OperationStatus.Error;
                return clientMessage;
            }

        }
    }
}
