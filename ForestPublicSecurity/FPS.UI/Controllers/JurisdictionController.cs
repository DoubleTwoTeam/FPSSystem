using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.Models;
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
            //AddAuthority();
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
    }
}