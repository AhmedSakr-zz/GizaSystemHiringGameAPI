using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using HiringGame.Common;
using HiringGame.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HiringGame.Controllers
{
    [Authorize]
    public class PlayersController: Controller
    {
        private readonly IPlayerServices _playerServices;

        public PlayersController(IPlayerServices playerServices)
        {
            _playerServices = playerServices;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = _playerServices.GetList();
            return View(model);
        }

        [HttpGet]
        public IActionResult ScoreDetails(int playerId)
        {
            var clientMessage = _playerServices.GetPlayerScore(playerId);
            if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
            {
                return View(clientMessage.ReturnedData);
            }

            TempData["clientMessage"] = Helper.PrepareClientMessage(
                string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
            return RedirectToAction("Index");
        }

        public IActionResult TopScore()
        {
            var model = _playerServices.GetList().OrderBy(t => t.Score).ToList();
            return View(model);
        }
    }
}
