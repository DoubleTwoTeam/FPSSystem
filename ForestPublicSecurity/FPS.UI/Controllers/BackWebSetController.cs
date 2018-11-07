using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FPS.UI.Controllers
{
    public class BackWebSetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Footer()
        {
            return View();
        }

        public IActionResult Left_menu()
        {
            return View();
        }

        public IActionResult Top()
        {
            return View();
        }
    }
}