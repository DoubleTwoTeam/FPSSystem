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
using Webdiyer.WebControls.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public InstanceController(IPoliceCase policeCase, IHostingEnvironment hostingEnvironment, IApprove approve, IPageHelper pageHelper)
        {
            _policeCase = policeCase;
            _hostingEnvironment = hostingEnvironment;
            _approve = approve;
            _pageHelper = pageHelper;
        }

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
        public IActionResult GetInstanceList(int id = 1)
        {
            PageList<InstanceDataModel> pageList = _policeCase.GetInstanceList();
            List<InstanceDataModel> list = pageList.ListData;
            PagedList<InstanceDataModel> pagedList = new PagedList<InstanceDataModel>(pageList.ListData, id, pageParams.PageSize);
            pagedList = pageList.ListData.ToPagedList(id - 1, pageParams.PageSize);
            pagedList.TotalItemCount = pageList.TotalCount;
            pagedList.CurrentPageIndex = id;
            return View(pagedList);
        }

        /// <summary>
        /// 查询与分页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NextPage(int id = 1)
        {
            pageParams.PageSize = pageSize;
            PageList<InstanceDataModel> pageList = _policeCase.GetInstanceList();
            PagedList<InstanceDataModel> pagedList = new PagedList<InstanceDataModel>(pageList.ListData, id, pageParams.PageSize);
            pagedList = pageList.ListData.ToPagedList(id - 1, pageParams.PageSize);
            pagedList.TotalItemCount = pageList.TotalCount;
            pagedList.CurrentPageIndex = id;
            return PartialView("_ShowInstance", pagedList);
        }

        /// <summary>
        /// 案件添加页面
        /// </summary>
        /// <returns></returns>
        public IActionResult InsertPoliceCase(int id)
        {
            HttpContext.Session.SetString("alterid", JsonConvert.SerializeObject(id));
            //绑定立案、结案下拉
            GetBusinesses();
            // 案件类型下拉（普通、重大、特大）
            GetInstanceState();
            return View();
        }

        /// <summary>
        /// 案件添加
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertPoliceCase(Instance instance)
        {
            //    long size = 0;
            //    var filename = ContentDispositionHeaderValue
            //                    .Parse(file.ContentDisposition)
            //                    .FileName
            //                    .Trim('"');
            //    //_hostingEnvironment.WebRootPath就是要存的地址可以改下
            //    filename = _hostingEnvironment.WebRootPath + $@"\{filename}";
            //    size = file.Length;
            //    using (FileStream fs = System.IO.File.Create(filename))
            //    {
            //        file.CopyTo(fs);
            //        fs.Flush();
            //    }

            var alterid = Convert.ToInt32(JsonConvert.DeserializeObject(HttpContext.Session.GetString("alterid")));
            //获取登录人信息
            var seesi = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<UserAndRole>(seesi);

            instance.RegisterPeopleID = user.ID;
            instance.AlterID = alterid;
            instance.ApproveState = 1;
            instance.Time = DateTime.Now;
            int result = _policeCase.InsertInstance(instance);
            if (result > 0)
            {
                //Approve approve = new Approve() { OriginalId = instance.ID, Ideas = "", State = Convert.ToString(instance.ApproveState), BusinesstypeId = 1 };
                Approve approve = _policeCase.GetApprove(instance);
                int i = _approve.InsertApprove(approve);
                if (i > 0)
                {
                    return Content("<script>alert('添加案情成功，等待审批');var index = parent.layer.getFrameIndex(window.name);window.parent.location.reload();parent.layer.close(index); location.href='/Alarm/CaptainOperation'</script>", "text/html;charset=utf-8");
                }
            }
            return Content("<script>alert('操作失败');var index = parent.layer.getFrameIndex(window.name);window.parent.location.reload();parent.layer.close(index); location.href='/Alarm/CaptainOperation'</script>", "text/html;charset=utf-8");
        }

        public void OverInstance(int id)
        {
            Instance instance = _policeCase.GetInstanceById(id);
            instance.InstanceTypes = 2;
            instance.ApproveState = 1;
            Approve approve = _policeCase.GetApprove(instance);
            int result = _approve.InsertApprove(approve);
            if (result > 0)
            {
                int i = _policeCase.UpdateinStance(instance);
                if (i > 0)
                {
                    Response.WriteAsync("<script>alert('申请结案成功！')</script>");
                }
                else
                {
                    Response.WriteAsync("<script>alert('案件转型失败！')</script>");
                }
            }
            else
            {
                Response.WriteAsync("<script>alert('申请结案失败！')</script>");
            }
        }

        /// <summary>
        /// 绑定立案、结案下拉
        /// </summary>
        public void GetBusinesses()
        {
            List<Business> list = _policeCase.GetBusinessesList();
            var linq = from s in list
                       select new SelectListItem
                       {
                           Text = s.Name,
                           Value = s.ID.ToString()
                       };
            ViewBag.businesses = linq.ToList();
        }

        /// <summary>
        /// 案件类型下拉（普通、重大、特大）
        /// </summary>
        public void GetInstanceState()
        {
            InstanceState[] instanceStates =
            {
                new InstanceState(){ID=1,Name="普通"},
                new InstanceState(){ID=2,Name="重大"},
                new InstanceState(){ID=3,Name="特大"}
            };
            var linq = from s in instanceStates
                       select new SelectListItem
                       {
                           Text = s.Name,
                           Value = s.ID.ToString()
                       };
            ViewBag.InstanceState = linq.ToList();
        }

    }
}