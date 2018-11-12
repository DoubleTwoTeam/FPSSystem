﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using FPS.Models.DTO;
using FPS.UI.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Webdiyer.WebControls.Mvc;

namespace FPS.UI.Controllers
{
    public class ApproveController : Controller
    {
        /// <summary>
        /// 权限
        /// </summary>
        private readonly IApprove _approve;

        /// <summary>
        /// 案件
        /// </summary>
        private readonly IPoliceCase _policeCase;

        /// <summary>
        /// 分页
        /// </summary>
        private readonly IPageHelper _pageHelper;

        /// <summary>
        /// 映射路径
        /// </summary>
        private readonly IHostingEnvironment _hostingEnvironment;
        
        string loginRoleId = "1";//当前用户的权限ID
        int pageSize = 8;//每页显示多少条数据

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="approve"></param>
        /// <param name="policeCase"></param>
        /// <param name="pageHelper"></param>
        public ApproveController(IApprove approve, IPoliceCase policeCase, IPageHelper pageHelper, IHostingEnvironment hostingEnvironment)
        {
            _approve = approve;
            _policeCase = policeCase;
            _pageHelper = pageHelper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 分页参数
        /// </summary>
        PageParams pageParams = new PageParams()
        {
            Fields = "Approve.ID,Instance.ID as InstanceID,Instance.RegisterPeopleID,Business.ID as BusinessID,Business.Name as BusinessName,Users.realName as UsersName,Role.Rolename as RoleName,Instance.InstanceTypes,Instance.Time as InstanceTime,Instance.ApproveState",
            TableName = " Approve,Users,Instance,Business,Role",
            Filter = " Approve.ORIGINALID=Instance.ID and Approve.BUSINESSTYPEID=Business.ID and Approve.APPROVEPEOPLEID=USERS.ID and Approve.ROLEID=Role.ID and Approve.State=1 ",
            Sort = " Approve.ID desc"
        };

        /// <summary>
        /// 审批页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GetApproveList(int id = 1)
        {
            string user = HttpContext.Session.GetString("user");
            UserAndRole userAndRole = JsonConvert.DeserializeObject<UserAndRole>(user);
            pageParams.CurPage = id;
            pageParams.Filter += "  and Approve.RoleId=" + userAndRole.RID;
            pageParams.PageSize = pageSize;
            PageList<ApproveDataModel> pageList =_approve.GetApproveList(pageParams);
            PagedList<ApproveDataModel> pagedList = new PagedList<ApproveDataModel>(pageList.ListData, id, pageParams.PageSize);
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
        public IActionResult NextPage(int id=1)
        {
            string user = HttpContext.Session.GetString("users");
            UserAndRole userAndRole = JsonConvert.DeserializeObject<UserAndRole>(user);
            pageParams.CurPage = id;
            pageParams.Filter += "  and Approve.RoleId=" + userAndRole.RID;
            pageParams.PageSize = pageSize;
            PageList<ApproveDataModel> pageList = _approve.GetApproveList(pageParams);
            PagedList<ApproveDataModel> pagedList = new PagedList<ApproveDataModel>(pageList.ListData, id, pageParams.PageSize);
            pagedList = pageList.ListData.ToPagedList(id - 1, pageParams.PageSize);
            pagedList.TotalItemCount = pageList.TotalCount;
            pagedList.CurrentPageIndex = id;
            return PartialView("_ShowApprove", pagedList);
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bussiness"></param>
        /// <returns></returns>
        public void PassApprove(int id, int bussiness, int inStanceId)
        {
            string user = HttpContext.Session.GetString("users");
            UserAndRole userAndRole = JsonConvert.DeserializeObject<UserAndRole>(user);
            int userID = userAndRole.ID;
            Approve approve = _approve.GetApproveById(id);
            ApproveCourse approveCourse = _approve.GetApproveCoursesList(approve.PlaceID);
            approve.Ideas = "";
            approve.State = "2";
            approve.ApprovePeopleId = userID;
            approve.Time = DateTime.Now;
            int i = _approve.UpdateApprove(approve);
            if (i > 0)
            {
                if (approveCourse != null)
                {
                    if (approveCourse.PostpositionID == 0)
                    {
                        //approve.Ideas = "";
                        //approve.State = "2";
                        //approve.ApprovePeopleId = userID;
                        //approve.Time = DateTime.Now;
                        //int result = _approve.UpdateApprove(approve);
                        //if (result > 0)
                        //{
                        Instance instance = _policeCase.GetInstanceById(inStanceId);
                        instance.ApproveState = 2;
                        int a = _policeCase.UpdateinStance(instance);
                        if (a > 0)
                        {
                            Content("<script>alert('审核通过！')</script>");
                        }
                        //}
                    }
                    else
                    {
                        Approve approves = new Approve() { BusinesstypeId = approve.BusinesstypeId, OriginalId = approve.OriginalId, PlaceID = approveCourse.PostpositionID, RoleId = approveCourse.ApproveRoleId, State = "1" };
                        int result = _approve.InsertApprove(approves);
                        if (result > 0)
                        {

                            Content("<script>alert('您的审核通过！正在进行下一级审核')</script>");
                            //PassApprove(approve.ID, approve.BusinesstypeId, inStanceId);
                        }
                    }
                }
            }
            else
            {
                Content("<script>alert('您的审核未成功')</script>");
            }
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inStanceId"></param>
        public void NoApprove(int id, int inStanceId)
        {
            string user = HttpContext.Session.GetString("users");
            UserAndRole userAndRole = JsonConvert.DeserializeObject<UserAndRole>(user);
            int userID = userAndRole.ID;
            Approve approve = _approve.GetApproveById(id);
            approve.ApprovePeopleId = userID;
            approve.State = "3";
            approve.Time = DateTime.Now;
            int result = _approve.UpdateApprove(approve);
            if (result > 0)
            {
                Instance instance = _policeCase.GetInstanceById(inStanceId);
                instance.ApproveState = 3;
                int i = _policeCase.UpdateinStance(instance);
                if (i > 0)
                {
                    Content("<script>alert('已驳回！')</script>");
                }
            }
            else
            {
                Content("<script>alert('驳回失败！')</script>");
            }
        }

        /// <summary>
        /// 获取案件详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GetInstanceById(int id)
        {
            GetBusinesses();
            GetInstanceState();
            InstanceDataModel instance = _approve.GetInstanceById(id);
            return View(instance);
        }

        /// <summary>
        /// 附件下载
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult DownLoad(string file)
        {
            var addrUrl = file;
            //转换成文件流
            var stream = System.IO.File.OpenRead(addrUrl);
            //获取文件的后缀名
            string fileExt = Path.GetExtension(addrUrl);
            //获取文件的ContentType
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(stream, memi, Path.GetFileName(addrUrl));
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
            ViewBag.InstanceTypes = linq.ToList();
        }
    }
}