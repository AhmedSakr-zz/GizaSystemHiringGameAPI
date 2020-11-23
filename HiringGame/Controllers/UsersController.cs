using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HiringGame.Application.Dtos;
using HiringGame.Common;
using HiringGame.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HiringGame.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = _mapper.Map<List<AppUserForReturnDto>>(_userManager.Users.AsQueryable().ToList());
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var user = new AppUserForReturnDto();
            return View("CreateEdit", user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserForReturnDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEdit", model);
            }

            var appUser = _mapper.Map<AppUser>(model);
            appUser.UserName = appUser.EmailAddress;

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("User created successfully", DataEnum.ClientMessageType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("CreateEdit", model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == id);
            if (user == null)
            {

                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("User id is not correct", DataEnum.ClientMessageType.Error);
                return RedirectToAction("Index");
            }
            else
            {
                return View("CreateEdit", _mapper.Map<AppUserForReturnDto>(user));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppUserForReturnDto model)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == model.Id);

            if (user == null)
            {

                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("User id is not correct", DataEnum.ClientMessageType.Error);
                return RedirectToAction("Index");
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var passToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, passToken, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("CreateEdit", model);
                }

            }

            var updateResult = await _userManager.UpdateAsync(_mapper.Map(model, user));
            if (updateResult.Succeeded)
            {
                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("User updated successfully", DataEnum.ClientMessageType.Success);
                return RedirectToAction("Index");
            }

            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("CreateEdit", model);



        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == id);

            if (user == null)
            {

                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("User id is not correct", DataEnum.ClientMessageType.Error);
                return RedirectToAction("Index");
            }

            var rootAdmin = _userManager.Users.OrderBy(t => t.Id).First();
            if (rootAdmin.Id == id)
            {
                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("Can not delete this user", DataEnum.ClientMessageType.Error);
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("User deleted successfully", DataEnum.ClientMessageType.Success);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index");
        }
    }
}
