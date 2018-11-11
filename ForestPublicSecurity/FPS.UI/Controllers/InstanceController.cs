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
using FPS.Models.DTO;
using FPS.UI.Common;

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

        private readonly IPageHelper _pageHelper;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="policeCase"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="approve"></param>
        /// <param name="pageHelper"></param>
        public InstanceController(IPoliceCase policeCase,IHostingEnvironment hostingEnvironment,IApprove approve, IPageHelper pageHelper)
        {
            _policeCase = policeCase;
            _hostingEnvironment = hostingEnvironment;
            _approve = approve;
            _pageHelper = pageHelper;
        }

        string loginRoleId = "1";//当前用户的权限ID
        int pageSize = 8;//每页显示多少条数据

        /// <summary>
        /// 分页参数
        /// </summary>
        PageParams pageParams = new PageParams()
        {
            Fields = "Instance.ID,Alarm.AlarmReason,Alarm.DetailSplace,Users.RealName,Instance.InstanceTypes,Instance.ApproveState,Instance.InstanceState,Instance.Time as InstanceTime,Instance.Space",
            TableName = " Instance,Alarm,Users",
            Filter = " Instance.AlterID=Alarm.ID and Instance.RegisterPeopleID=Users.ID",
            Orderby = "  Instance.ID desc"
        };

        /// <summary>
        /// 案件显示页面
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInstanceList(int id=1)
        {
            pageParams.CurPage = id;
            pageParams.PageSize = pageSize;
            PageList<InstanceDataModel> pageList = _pageHelper.InfoList<InstanceDataModel>(pageParams);
            List<InstanceDataModel> list = pageList.ListData;
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