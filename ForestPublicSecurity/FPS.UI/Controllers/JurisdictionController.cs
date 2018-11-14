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
using FPS.UI.Filter;
using Microsoft.AspNetCore.Http;

namespace FPS.UI.Controllers
{
    [PermissionFilter]
    public class JurisdictionController : Controller
    {
        /// <summary>
        /// 依赖注入接口
        /// </summary>
        public IJurisdiction _jurisdiction { get; set; }
        public JurisdictionController(IJurisdiction jurisdiction) => _jurisdiction = jurisdiction;
        public IActionResult Index()
        {
            return View();
        }
        #region 权限增删查改
        /// <summary>
        /// 权限显示
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAuthority()
        {
            List<Authority> authority = new List<Authority>();
            authority = _jurisdiction.GetAuthority();
            return View(authority);
        }
        [HttpPost]
        public IActionResult GetAuthority(int id = 1)
        {
            List<Authority> authorities = new List<Authority>();
            authorities = _jurisdiction.GetAuthority();
            return PartialView("_ShowGetAuthority", authorities);
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
        public IActionResult AddAuthority(string newname, string newurl, int authority, int fatherid)
        {
            AddAuthority();

            Authority authoritys = new Authority();

            authoritys.Name = newname;
            authoritys.FatherId = fatherid;
            authoritys.State = 1;
            authoritys.Url = newurl;
            authoritys.OrderId = 0;

            int i = _jurisdiction.AddAuthority(authoritys);

            if (i > 0)
            {
                return Content("<script>alert('新权限添加成功!');location.href='/Jurisdiction/GetAuthority'</script>", "text/html;charset=utf-8");
            }
            return Content("<script>alert('新权限添加失败!');location.href='/Jurisdiction/GetAuthority'</script>", "text/html;charset=utf-8");
        }

        /// <summary>
        /// 获取权限下拉
        /// </summary>
        /// <param name="gid"></param>
        public void GetAuthorityList(int gid = 0)
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
        /// 修改权限反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateAuthorityShow(int id)
        {
            GetAuthorityList();
            Authority authority = _jurisdiction.UpdateAuthorityShow(id);
            return View(authority);
        }

        /// <summary>
        /// 修改权限保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpdateAuthorityShow(Authority authoritymodel,int Authority)
        {
            GetAuthorityList();
            authoritymodel.FatherId = Authority;
            int i = _jurisdiction.UpdateAuthority(authoritymodel);
            return i;
        }
        #endregion

        #region 角色增删查改
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
            List<RoleAndAuthority> roleAuthorities = new List<RoleAndAuthority>();
            roleAuthorities = _jurisdiction.GetRole();
            return View(roleAuthorities);
        }

        [HttpPost]
        public IActionResult GetRole(int id = 1)
        {
            List<UserAndRole> roleAuthorities = new List<UserAndRole>();
            roleAuthorities = _jurisdiction.ShowUserAndRole();
            return PartialView("_ShowGetRole", roleAuthorities);
        }

        /// <summary>
        /// 获取角色下拉
        /// </summary>
        /// <param name="gid"></param>
        public void GetRoleList(int gid = 0)
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
        /// 修改角色反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateRoleShow(int id)
        {
            GetAuthorityList();
            RoleAndAuthority roleAndAuthority = _jurisdiction.UpdateRoleShow(id);
            return View(roleAndAuthority);
        }

        /// <summary>
        /// 修改角色保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpdateRoleShow(Authority authority, string qxid, int id)
        {
            GetAuthorityList();
            int i = _jurisdiction.UpdateRole(authority, qxid.ToString(), id);
            return i;
        }
        /// <summary>
        /// 修改角色状态为停用
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRole(string id)
        {
            int state = _jurisdiction.DeleteRole(id);
        }
        #endregion

        #region 用户增删查改
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
            int i = _jurisdiction.AddUser(users, role);
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
        public IActionResult GetUser(int id = 1)
        {
            List<UserAndRole> userAndRoles = new List<UserAndRole>();
            userAndRoles = _jurisdiction.ShowUserAndRole();
            return PartialView("_ShowGetUser", userAndRoles);
        }

        /// <summary>
        /// 修改用户状态为停用
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="byid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteUser(string tablename, string byid, string id)
        {
            int state = _jurisdiction.DeleteUser(tablename, byid, id);
        }

        /// <summary>
        /// 修改用户反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateUserShow(int id)
        {
            GetRoleList();
            UserAndRole userAndRole = _jurisdiction.UpdateUserShow(id);
            return View(userAndRole);
        }

        /// <summary>
        /// 修改用户保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="users"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpdateUserShow(Users users, int role, int id)
        {
            GetRoleList();
            int i = _jurisdiction.UpdateUser(users, role.ToString(), id);
            return i;
        }
        #endregion
    }
}