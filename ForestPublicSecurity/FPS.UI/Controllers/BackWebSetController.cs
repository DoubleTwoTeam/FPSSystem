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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using FPS.UI.Common;
using Microsoft.AspNetCore.Authentication;

namespace FPS.UI.Controllers
{
    public class BackWebSetController : Controller
    {
        private readonly IStudent _student;

        private readonly IJurisdiction _jurisdiction;
        private ICacheService _cacheService { get; set; }

        public BackWebSetController(IStudent student, IJurisdiction jurisdiction, ICacheService cacheService)
        {
            _jurisdiction = jurisdiction;
            _cacheService = cacheService;
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
            var id=HttpContext.User.Identity.Name;
            List<Authority> authority = new List<Authority>();
            authority = _student.GetAuthority(Convert.ToInt32(id));
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
        public  IActionResult Login(string name,string pwd)
        {
            UserAndRole users =_student.Login(name,pwd);
            if (users==null)
                return Content("<script>alert('登录失败,请检查账号密码!');</script>", "text/html;charset=utf-8");
            //Session存储用户信息
            HttpContext.Session.SetString("user",JsonConvert.SerializeObject(users));

            //构造ClaimsIdentity 对象
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //创建 Claim 类型,传入 ClaimsIdentity 中
            identity.AddClaim(new Claim(ClaimTypes.Name, users.ID.ToString()));
            //创建ClaimsPrincipal对象,传入ClaimsIdentity 对象,调用HttpContext.SignInAsync完成登录
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            //存Redis
            RedisHelper.Set<UserAndRole>(users.LoginName, users);
            //取Redis
            var user2 = RedisHelper.Get<UserAndRole>(users.LoginName);

            ////存储redis
            //_cacheService.Add(users.LoginName, JsonConvert.SerializeObject(users));
            ////取Redis
            //var result = _cacheService.Get(HttpContext.User.Claims.First().Value);

            //.net Core返回一个弹窗需要制定文本类型与编码格式
            return Content("<script>alert('登录成功');location.href='/BackWebSet/Index'</script>", "text/html;charset=utf-8");
        }
    }
}