using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.Models;
using FPS.UI;
using FPS.IServices;
using FPS.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPS.UI.Controllers
{
    public class JurisdictionController : Controller
    {
        public IJurisdiction _jurisdiction { get; set; }

        public JurisdictionController(IJurisdiction jurisdiction) => _jurisdiction = jurisdiction;
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <returns></returns>
        public IActionResult AddAuthority()
        {
             GetAuthorityList();
            return View();
        }
        [HttpPost]
        public IActionResult AddAuthority(string newname, string newurl, int authority,int fatherid)
        {
            AddAuthority();

            Authority authoritys = new Authority();

            authoritys.Name = newname;
            authoritys.FatherId = fatherid;
            authoritys.State = 1;
            authoritys.Url = newurl;
            authoritys.OrderId = 0;

            int i = _jurisdiction.AddAuthority(authoritys);

            if(i>0)
            {
                return Content("<script>alert('新权限添加成功!');location.href='/Jurisdiction/Index'</script>");
            }
            else
            {
                return Content("<script>alert('新权限添加失败!');location.href='/Jurisdiction/AddDroit'</script>");
            };
        }

        /// <summary>
        /// 获取权限下拉
        /// </summary>
        /// <param name="gid"></param>
        public void GetAuthorityList(int gid=0)
        {
            List<Authority> list = new List<Authority>();
            list = _jurisdiction.GetAuthorityList(gid);
            var linq = from s in list
                       select new SelectListItem
                       {
                           Text = s.Name,
                           Value = s.ID.ToString()
                       };
            ViewBag.Authority = linq.ToList();
        }

       /// <summary>
       /// 添加角色&&角色赋予权限显示
       /// </summary>
       /// <returns></returns>
        public IActionResult AddRole()
        {
            GetRoleList();
            List<Authority> authoritylist = new List<Authority>();
            authoritylist = _jurisdiction.GetAuthority();
            return View(authoritylist);
        }
        [HttpPost]
        public int AddRole(string name, string qxid)
        {
            AddRole();
            int i = _jurisdiction.AddRole(name, qxid);
            return i;
        }

        /// <summary>
        /// 显示角色
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRole()
        {
            List<Role> role = new List<Role>();
            role = _jurisdiction.GetRole();
            return View(role);
        }

        /// <summary>
        /// 获取角色下拉
        /// </summary>
        /// <param name="gid"></param>
        public void GetRoleList(int gid=0)
        {
            List<Role> list = new List<Role>();
            list = _jurisdiction.GetRoleList(gid);
            var linq = from s in list
                       select new SelectListItem
                       {
                           Text = s.RoleName,
                           Value = s.ID.ToString()
                       };
            ViewBag.role = linq.ToList();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            GetRoleList();
            return View();
        }
        [HttpPost]
        public int AddUser(Users users, string role)
        {
            GetRoleList();
            int i = _jurisdiction.AddUser(users,role);
            return i;
        }

        /// <summary>
        /// 显示用户
        /// </summary>
        /// <returns></returns>
        public IActionResult GetUser()
        {
            List<UserAndRole> userAndRoles = new List<UserAndRole>();
            userAndRoles = _jurisdiction.ShowUserAndRole();
            return View(userAndRoles);
        }
        /// <summary>
        /// 显示用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetUser(int id=1)
        {
            List<UserAndRole> userAndRoles = new List<UserAndRole>();
            userAndRoles = _jurisdiction.ShowUserAndRole();
            return PartialView("_ShowGetUser", userAndRoles);
        }
    }
}