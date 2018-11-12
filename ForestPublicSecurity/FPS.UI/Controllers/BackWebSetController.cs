using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.Models;
using FPS.Services;
using FPS.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;

namespace FPS.UI.Controllers
{
    public class BackWebSetController : Controller
    {
        private readonly IJurisdiction _jurisdiction;
        private readonly IStudent _student;

        public BackWebSetController(IStudent student, IJurisdiction jurisdiction)
        {
            _jurisdiction = jurisdiction;
            _student = student;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //var user = JsonConvert.DeserializeObject<Users>(HttpContext.Session.GetString("user"));
            //return Content(string.Format("<script>alert('获取用户信息成功,用户名:{0}');</script>",user.LoginName), "text/html;charset=utf-8");
            return View();
        }

        /// <summary>
        /// 中心页
        /// </summary>
        /// <returns></returns>
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// 底部页
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 顶部页
        /// </summary>
        /// <returns></returns>
        public IActionResult Top()
        {
            return View();
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(string name,string pwd)
        {
            UserAndRole users =_student.Login(name,pwd);
            if (users==null)
                return Content("<script>alert('登录失败,请检查账号密码!');</script>", "text/html;charset=utf-8");
            //存储用户信息
            HttpContext.Session.SetString("user",JsonConvert.SerializeObject(users));

            //.net Core返回一个弹窗需要制定文本类型与编码格式
            return Content("<script>alert('登录成功');location.href='/BackWebSet/Index'</script>", "text/html;charset=utf-8");
        }
    }
}