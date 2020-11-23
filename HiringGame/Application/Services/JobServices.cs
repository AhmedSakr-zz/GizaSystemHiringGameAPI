using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using HiringGame.Common;
using HiringGame.DataModel;
using Microsoft.EntityFrameworkCore;

namespace HiringGame.Application.Services
{
    public class JobServices : IJobServices
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public JobServices(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IList<JobDto> GetList()
        {
            var allCategories = _dbContext.Jobs.AsQueryable().ToList();
            return _mapper.Map<List<JobDto>>(allCategories);
        }

        public ClientMessage<int> Create(JobDto model)
        {
            var clientMessage = new ClientMessage<int>();

            try
            {
                var job = _mapper.Map<Job>(model);
                _dbContext.Jobs.Add(job);
                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Job created successfully" };
                clientMessage.ReturnedData = job.Id;
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

        public ClientMessage<JobDto> GetById(int id)
        {
            var clientMessage = new ClientMessage<JobDto>();


            var job = _dbContext.Jobs.FirstOrDefault(t => t.Id == id);

            if (job == null)
            {
                clientMessage = new ClientMessage<JobDto>
                {
                    ClientStatusCode = DataEnum.OperationStatus.Error,
                    ClientMessageContent = new List<string> { "Id is not correct" }
                };
                return clientMessage;
            }


            clientMessage.ReturnedData = _mapper.Map<JobDto>(job);
            clientMessage.ClientStatusCode = DataEnum.OperationStatus.Ok;
            return clientMessage;



        }

         

        public ClientMessage<int> Update(JobDto model)
        {
            var clientMessage = new ClientMessage<int>();

            try
            {
                var job = _dbContext.Jobs.Find(model.Id);
                job.Name = model.Name;
                job.IsActive = model.IsActive;
                _dbContext.SaveChanges();

                clientMessage.ClientMessageContent = new List<string> { "Job updated successfully" };
                clientMessage.ReturnedData = job.Id;
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
