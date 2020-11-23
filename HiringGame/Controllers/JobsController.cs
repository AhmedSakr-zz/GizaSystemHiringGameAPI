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
    public class JobsController : Controller
    {
        private readonly IJobServices _jobServices;
        private readonly UserManager<AppUser> _userManager;

        public JobsController(IJobServices jobServices, UserManager<AppUser> userManager)
        {
            _jobServices = jobServices;
            _userManager = userManager;
        }
        public IActionResult Index(int status = 3)
        {
            var active = status == 1;

            var model = _jobServices.GetList().Where(t => t.IsActive == active || status == 3).ToList();
            ViewBag.selecteStatus = status;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var loggedUserId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var model = new JobDto
            {
                CreatedById = loggedUserId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            return View("CreateEdit", model);
        }

        [HttpPost]
        public IActionResult Create(JobDto model)
        {
            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            var clientMessage = _jobServices.Create(model);

            if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
            {
                TempData["clientMessage"] = Helper.PrepareClientMessage(
                    string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Success);
                return RedirectToAction("Index");
            }

            else
            {
                TempData["clientMessage"] = Helper.PrepareClientMessage(
                    string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
                return View("CreateEdit", model);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var clientMessage = _jobServices.GetById(id);
            if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
            {
                return View("CreateEdit", clientMessage.ReturnedData);
            }

            TempData["clientMessage"] = Helper.PrepareClientMessage(
                string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(JobDto model)
        {
            if (!ModelState.IsValid)
                return View("CreateEdit", model);

            model.UpdatedById = -1;
            model.UpdatedDate = DateTime.Now;
            if (User.Identity.Name != null)
                model.CreatedById = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;

            var clientMessage = _jobServices.Update(model);

            if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
            {
                TempData["clientMessage"] = Helper.PrepareClientMessage(
                    string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Success);
                return RedirectToAction("Index");
            }

            else
            {
                TempData["clientMessage"] = Helper.PrepareClientMessage(
                    string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
                return View("CreateEdit", model);
            }

        }
    }
}
