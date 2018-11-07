using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FPS.Services;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

namespace FPS.UI.Controllers
{
    public class CenterController : Controller
    {
        private IHostingEnvironment hostingEnvironment;
        private readonly IStudent _student;

        public CenterController(IStudent student, IHostingEnvironment env)
        {
            _student = student;
            this.hostingEnvironment = env;
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
        /// 后台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult BackWeb()
        {
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

        /// <summary>
        /// 报警页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CallPolice()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// 报警信息提取
        /// </summary>
        /// <returns></returns>
        public ActionResult CallPolice(Alarm alarm, IFormFile fileinput)
        {
            // 文件大小
            long size = 0;
            // 原文件名（包括路径）
            var filename = ContentDispositionHeaderValue.Parse(fileinput.ContentDisposition).FileName;
            // 扩展名
            var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "");
            // 新文件名
            string shortfilename = $"{Guid.NewGuid()}{extName}";
            // 新文件名（包括路径）
            filename = hostingEnvironment.WebRootPath + @"\upload\" + shortfilename;
            // 设置文件大小
            //size += signature.Length;
            // 创建新文件
            using (FileStream fs = System.IO.File.Create(filename))
            {
                // 复制文件
                fileinput.CopyTo(fs);
                // 清空缓冲区数据
                fs.Flush();
            }
            return View();
        }
    }
}