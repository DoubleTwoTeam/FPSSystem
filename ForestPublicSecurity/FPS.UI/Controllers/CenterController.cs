﻿using System;
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
using FPS.Models.DTO;
using System.Data.OracleClient;
using System.Data;
using Newtonsoft.Json;
using FPS.UI.Common;

namespace FPS.UI.Controllers
{
    public class CenterController : Controller
    {
        private IHostingEnvironment hostingEnvironment;
        private IPageHelper _pageHelper;
        private readonly IStudent _student;

        public CenterController(IStudent student, IPageHelper pageHelper, IHostingEnvironment env)
        {
            _pageHelper = pageHelper;
            _student = student;
            this.hostingEnvironment = env;
        }

        /// <summary>
        /// 森林管控中心首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            PageParams pageParams = new PageParams() { CurPage=1,Fields="ID,Name,Sex",Filter="",PageSize=4,Orderby="ID desc",TableName="student"};
            var result=_pageHelper.InfoList<Student>(pageParams);

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
            //默认值(null阻断)
            alarm.Space = "";
            alarm.Url = "";
            alarm.Time = DateTime.Now;
            // 文件大小
            //long size = 0;
            // 原文件名（包括路径）
            var filename = ContentDispositionHeaderValue.Parse(fileinput.ContentDisposition).FileName;
            // 扩展名
            var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "");
            // 新文件名
            string shortfilename = $"{Guid.NewGuid()}{extName}";
            // 新文件名（包括路径）
            filename = hostingEnvironment.WebRootPath + @"\Images\" + shortfilename;
            // 设置文件大小
            //size += signature.Length;
            // 创建新文件

            //数据库添加对象
            alarm.Enclosure = shortfilename;
            var result = _student.AddCallPolice(alarm);
            if (result>0)
            {
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    // 复制文件
                    fileinput.CopyTo(fs);
                    // 清空缓冲区数据
                    fs.Flush();
                }
                return Content("<script>alert('报案成功,请保护好自己,耐心等待周队长处理!');location.href='/Center/Index'</script>", "text/html;charset=utf-8");
            }
            else
            {
                return Content("<script>alert('报案失败,请检查网络!如遇经济情况请直接联系周队长: 18513121113');location.href='/Center/Index'</script>", "text/html;charset=utf-8");
            }
        }
    }
}