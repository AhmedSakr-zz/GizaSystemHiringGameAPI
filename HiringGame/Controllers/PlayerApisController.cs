using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using HiringGame.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HiringGame.Controllers
{

    [AllowAnonymous]
    [EnableCors("AllowOrigin")]
    public class PlayerApisController : ControllerBase
    {
        private readonly IPlayerServices _playerServices;
        private readonly IJobServices _jobServices;
        private readonly IVideoServices _videoServices;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public PlayerApisController(IPlayerServices playerServices,
            IJobServices jobServices,
            IVideoServices videoServices,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _playerServices = playerServices;
            _jobServices = jobServices;
            _videoServices = videoServices;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/api/players/register")]
        public IActionResult Register(PlayerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clientMessage = _playerServices.Register(dto);

            return clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok
                ? Ok(clientMessage.ReturnedData)
                : StatusCode((int)HttpStatusCode.InternalServerError, clientMessage.ClientMessageContent);
        }

        [HttpPost]
        [Route("/api/players/Level2Answer")]
        public IActionResult Level2Answer([FromBody] TransactionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var clientMessage = _playerServices.CreateLevel2Transaction(dto);

            return clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok
               ? Ok(clientMessage.ReturnedData)
                : StatusCode((int)HttpStatusCode.InternalServerError, clientMessage.ClientMessageContent);
        }

        [HttpPost]
        [Route("/api/players/Level3Answer")]
        public IActionResult Level3Answer([FromBody] List<TransactionDto> dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var clientMessage = _playerServices.CreateLevel3Transaction(dto);

            return clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok
                ? Ok(clientMessage.ReturnedData)
                : StatusCode((int)HttpStatusCode.InternalServerError, clientMessage.ClientMessageContent);
        }

        [HttpGet]
        [Route("/api/Jobs/GetActiveJobs")]
        public IActionResult GetActiveJobs()
        {
            var jobList = _jobServices.GetList().Where(t => t.IsActive);

            return Ok(jobList);
        }

        [HttpGet]
        [Route("/api/Questions/Level2")]
        public IActionResult GetLevel2Questions()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clientMessage = _playerServices.GetLevel2Questions();

            return clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok
                ? Ok(clientMessage.ReturnedData)
                : StatusCode((int)HttpStatusCode.InternalServerError, clientMessage.ClientMessageContent);
        }

        [HttpGet]
        [Route("/api/Questions/Level3")]
        public IActionResult GetLevel3Questions()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clientMessage = _playerServices.GetLevel3Questions();

            return clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok
                ? Ok(clientMessage.ReturnedData)
                : StatusCode((int)HttpStatusCode.InternalServerError, clientMessage.ClientMessageContent);
        }

        [HttpGet]
        [Route("/api/GameVideos")]
        public IActionResult GameVideos()
        {
            var videoList = _videoServices.GetList().Where(t => t.IsActive);
            var videoListForReturn = _mapper.Map<List<VideoForReturnDto>>(videoList);
            foreach (var videoForReturnDto in videoListForReturn)
            {
                videoForReturnDto.Url = _configuration["WebsiteUrl"] + "/App_Files/" + videoForReturnDto.Url;
            }

            return Ok(videoListForReturn);
        }

        [HttpPost]
        [Route("/api/players/SubmitTotalScore")]
        public IActionResult SubmitTotalScore(SubmitScoreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clientMessage = _playerServices.SubmitTotalScore(dto);

            return clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok
                ? Ok(clientMessage.ReturnedData)
                : StatusCode((int)HttpStatusCode.InternalServerError, clientMessage.ClientMessageContent);
        }

    }
}
