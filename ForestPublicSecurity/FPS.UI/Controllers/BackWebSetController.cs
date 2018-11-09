using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.Models;
using FPS.Services;
using FPS.IServices;

namespace FPS.UI.Controllers
{
    public class BackWebSetController : Controller
    {

        public IJurisdiction _jurisdiction { get; set; }
        public BackWebSetController(IJurisdiction jurisdic) => _jurisdiction = jurisdic;
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
        /// <summary>
        /// 权限显示
        /// </summary>
        /// <returns></returns>
        public IActionResult Left_menu()
        {
            List<Authority> authority = new List<Authority>();
            authority = _jurisdiction.GetAuthority();
            return View(authority);
        }

        public IActionResult Top()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string name,string pwd)
        {
            //.net Core返回一个弹窗需要制定文本类型与编码格式
            return Content("<script>alert('登录成功');location.href='/BackWebSet/Index'</script>", "text/html;charset=utf-8");
        }
    }
}