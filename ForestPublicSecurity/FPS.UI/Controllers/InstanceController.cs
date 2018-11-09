using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FPS.UI.Controllers
{
    public class InstanceController : Controller
    {
        /// <summary>
        /// 依赖注入案件接口
        /// </summary>
        private readonly IPoliceCase _policeCase;

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IApprove _approve;

        public InstanceController(IPoliceCase policeCase,IHostingEnvironment hostingEnvironment,IApprove approve)
        {
            _policeCase = policeCase;
            _hostingEnvironment = hostingEnvironment;
            _approve = approve;
        }

        /// <summary>
        /// 案件显示页面
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInstanceList()
        {
            List<InstanceDataModel> list= _policeCase.GetInstanceList();
            return View(list);
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
        public IActionResult InsertPoliceCase(Instance instance,IFormFile file)
        {
            long size = 0;
            var filename = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                            .Trim('"');
            //_hostingEnvironment.WebRootPath就是要存的地址可以改下
            filename = _hostingEnvironment.WebRootPath + $@"\{filename}";
            size = file.Length;
            using (FileStream fs = System.IO.File.Create(filename))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            instance.Space = filename;
            instance.ApproveState = 0;
            instance.InstanceState = 0;
            instance.Time = DateTime.Now;
            int result= _policeCase.InsertInstance(instance);
            if (result > 0)
            {
                //Approve approve = new Approve() { OriginalId = instance.ID, Ideas = "", State = Convert.ToString(instance.ApproveState), BusinesstypeId = 1 };
                Approve approve = _policeCase.GetApprove(instance);
                int i= _approve.InsertApprove(approve);
                if (i>0)
                {
                    Response.WriteAsync("<script>alert('添加案情成功，等待审批')<script>");
                }
            }
            return View();
        }
        
    }
}