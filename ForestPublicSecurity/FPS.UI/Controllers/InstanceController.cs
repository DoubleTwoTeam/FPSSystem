using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace FPS.UI.Controllers
{
    public class InstanceController : Controller
    {
        /// <summary>
        /// 依赖注入案件接口
        /// </summary>
        private readonly IPoliceCase _policeCase;

        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="policeCase"></param>
        public InstanceController(IPoliceCase policeCase,IHostingEnvironment hostingEnvironment)
        {
            _policeCase = policeCase;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 案件显示页面
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInstanceList()
        {
            return View();
        }

        /// <summary>
        /// 案件添加页面
        /// </summary>
        /// <returns></returns>
        public IActionResult InsertPoliceCase()
        {
            return View();
        }

        /// <summary>
        /// 案件添加
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertPoliceCase(Instance instance, IFormFile file)
        {
            long size = 0;
            var filename = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                            .Trim('"');
            //_hostingEnvironment.WebRootPath就是要存的地址可以改下
            filename = _hostingEnvironment.WebRootPath + $@"\{filename}";
            size += file.Length;
            using (FileStream fs = System.IO.File.Create(filename))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return View();
        }

        public int InsertApprove(int id)
        {
            return 0;
        }
    }
}