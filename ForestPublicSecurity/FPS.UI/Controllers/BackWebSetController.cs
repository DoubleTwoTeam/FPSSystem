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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string name,string pwd)
        {
            //.net Core返回一个弹窗需要制定文本类型与编码格式
            return Content("<script>alert('登录');location.href='/BackWebSet/Index'</script>", "text/html;charset=utf-8");
        }
    }
}