using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiringGame.Application.Dtos;
using HiringGame.Application.Interfaces;
using HiringGame.Common;
using HiringGame.DataModel;
using HiringGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HiringGame.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly IQuestionServices _questionServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJobServices _jobServices;

        public QuestionsController(IQuestionServices questionServices,
            UserManager<AppUser> userManager,
            IJobServices jobServices)
        {
            _questionServices = questionServices;
            _userManager = userManager;
            _jobServices = jobServices;
        }
        [HttpGet]
        public IActionResult Index(int typeId = 1)
        {
            var model = new QuestionsIndexViewModel
            {
                Questions = _questionServices.GetByWhere(t => t.QuestionTypeId == typeId),
                TypeId = typeId
            };
            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> Create(int typeId = 1)
        //{
        //    var model = new QuestionDto
        //    {
        //        CreatedById = -1,
        //        CreatedDate = DateTime.Now,
        //        IsActive = true,
        //        QuestionTypeId = typeId

        //    };
        //    if (User.Identity.Name != null)
        //        model.CreatedById = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;

        //    return View("CreateEdit", model);
        //}

        //[HttpPost]
        //public IActionResult Create(QuestionDto model)
        //{
        //    model.Answers = model.Answers.Where(t => !string.IsNullOrWhiteSpace(t.AnswerString)).ToList();

        //    var clientMessage = _questionServices.Create(model);

        //    if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
        //    {
        //        TempData["clientMessage"] = Helper.PrepareClientMessage(
        //            string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Success);
        //        return RedirectToAction("Index", new { typeId = model.QuestionTypeId });
        //    }

        //    else
        //    {
        //        TempData["clientMessage"] = Helper.PrepareClientMessage(
        //            string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
        //        return View("CreateEdit", model);
        //    }

        //}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var clientMessage = _questionServices.GetById(id);
            if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
            {
                return View("CreateEdit", clientMessage.ReturnedData);
            }

            TempData["clientMessage"] = Helper.PrepareClientMessage(
                string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Error);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(QuestionDto model)
        {

            model.Answers = model.Answers.Where(t => !string.IsNullOrWhiteSpace(t.AnswerString)).ToList();

            model.UpdatedById = -1;
            model.UpdatedDate = DateTime.Now;
            if (User.Identity.Name != null)
                model.UpdatedById = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;

            var clientMessage = _questionServices.Update(model);

            if (clientMessage.ClientStatusCode == DataEnum.OperationStatus.Ok)
            {
                TempData["clientMessage"] = Helper.PrepareClientMessage(
                    string.Join(", ", clientMessage.ClientMessageContent), DataEnum.ClientMessageType.Success);
                return RedirectToAction("Index", new { typeId = model.QuestionTypeId });
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
