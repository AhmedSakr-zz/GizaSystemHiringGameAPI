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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HiringGame.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailServices;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            IConfiguration configuration,
            IEmailServices emailServices,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailServices = emailServices;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {


            //validate model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Correct login details
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);



            //User has access to system
            if (result.Succeeded)
            {
                return RedirectToAction("index", "Home");
            }
            //User doesn't have access to system
            else
            {
                ModelState.AddModelError("", "Invalid login attempt, please contact IT team.");
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "Account");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View(new ForgetPasswordDto());
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        {

            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {

                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("Invalid email address.", DataEnum.ClientMessageType.Error);
                return View(model);

            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var message = "Dear: " + model.EmailAddress;
            message += "<br/>Please follow the link below to reset your password<br>";
            message += _configuration["WebsiteUrl"] + "/home/resetpassword?token=" + token + "&email=" + model.EmailAddress;

            var messageParams = new SendMailParamsDto
            {
                ToList = new List<string> { model.EmailAddress },
                IsHtml = true,
                MessageBody = message,
                MessageTitle = _configuration["WebsiteName"] + ": Reset password"
            };
            var result = await _emailServices.SendMailAsync(messageParams);
            if (result)
            {
                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("Reset password link sent to your email address.",
                        DataEnum.ClientMessageType.Success);
                return View();
            }

            TempData["clientMessage"] = Helper.PrepareClientMessage("An error occured while sending the email.",
                DataEnum.ClientMessageType.Error);

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordDto
            {
                Token = token,
                EmailAddress = email
            });
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("Password and password confirmation not matched.",
                        DataEnum.ClientMessageType.Error);
                return View(model);

            }

            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {

                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("Invalid email address.", DataEnum.ClientMessageType.Error);
                return View(model);

            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData["clientMessage"] =
                    Helper.PrepareClientMessage("Your password changed successfully.",
                        DataEnum.ClientMessageType.Success);
                return View(model);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }



        public async Task<IActionResult> seedingadmins()
        {
            var user = new AppUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                FirstName = "Ahmed",
                LastName = "Sakr"
            };
            var result = await _userManager.CreateAsync(user, "Admin@123");
            return Json("Done");
        }
    }
}
