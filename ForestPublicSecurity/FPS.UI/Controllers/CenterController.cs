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

        public CenterController(IStudent student)
        {
            _student = student;
        }

        /// <summary>
        /// 森林管控中心首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Name = _student.GetStudentName();
            return View();
        }

        /// <summary>
        /// 后台登录入口
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            ViewBag.Name = _student.GetStudentName();
            return View();
        }

        /// <summary>
        /// 后台静态页
        /// </summary>
        /// <returns></returns>
        public ActionResult StaticIndex()
        {
            return View();
        }
    }
}