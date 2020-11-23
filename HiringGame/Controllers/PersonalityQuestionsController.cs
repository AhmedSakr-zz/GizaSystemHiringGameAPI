using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HiringGame.Controllers
{
    public class PersonalityQuestionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
