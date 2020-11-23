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
    public class VideosController : Controller
    {
        private readonly IVideoServices _videoServices;
        private readonly UserManager<AppUser> _userManager;

        public VideosController(IVideoServices videoServices, UserManager<AppUser> userManager)
        {
            _videoServices = videoServices;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    var model = _videoServices.GetList().ToList();
        //    return View(model);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{

        //    var loggedUserId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
        //    var model = new VideoDto
        //    {
        //        CreatedById = loggedUserId,
        //        CreatedDate = DateTime.Now,
        //        IsActive = true
        //    };

        //    return View("CreateEdit", model);
        //}

        //[HttpPost]
        //public IActionResult Create(VideoDto model)
        //{
        //    if (!ModelState.IsValid)
        //        return View("CreateEdit", model);

        //    var clientMessage = _videoServices.Create(model);

        //    if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
        //    {
        //        TempData["clientMessage"] = Helper.PrepareClientMessage(
        //            string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Success);
        //        return RedirectToAction("Index");
        //    }

        //    else
        //    {
        //        TempData["clientMessage"] = Helper.PrepareClientMessage(
        //            string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
        //        return View("CreateEdit", model);
        //    }

        //}

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var clientMessage = _videoServices.GetById(id);
        //    if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
        //    {
        //        return View("CreateEdit", clientMessage.ReturnedData);
        //    }

        //    TempData["clientMessage"] = Helper.PrepareClientMessage(
        //        string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
        //    return RedirectToAction("Index");
        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(VideoDto model)
        //{
        //    if (!ModelState.IsValid)
        //        return View("CreateEdit", model);

        //    model.UpdatedById = -1;
        //    model.UpdatedDate = DateTime.Now;
        //    if (User.Identity.Name != null)
        //        model.CreatedById = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;

        //    var clientMessage = _videoServices.Update(model);

        //    if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
        //    {
        //        TempData["clientMessage"] = Helper.PrepareClientMessage(
        //            string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Success);
        //        return RedirectToAction("Index");
        //    }

        //    else
        //    {
        //        TempData["clientMessage"] = Helper.PrepareClientMessage(
        //            string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
        //        return View("CreateEdit", model);
        //    }

        //}
    }
}
