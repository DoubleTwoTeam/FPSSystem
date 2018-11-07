using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FPS.UI.Controllers
{
    public class InstanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}