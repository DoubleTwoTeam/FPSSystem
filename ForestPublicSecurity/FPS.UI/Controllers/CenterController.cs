using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FPS.Services;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;

namespace FPS.UI.Controllers
{
    public class CenterController : Controller
    {
        private readonly IStudent _student;

        public CenterController(IStudent student) => _student = student;

        public IActionResult Index()
        {
            ViewBag.Name = _student.GetStudentName();
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Name = _student.GetStudentName();
            return View();
        }

        public ActionResult StaticIndex()
        {
            return View();
        }
    }
}