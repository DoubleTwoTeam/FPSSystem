using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using FPS.Models.DTO;
using FPS.UI.Common;


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


        string loginRoleId = "1";//当前用户的权限ID
        int pageSize = 8;//每页显示多少条数据
        
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="approve"></param>
        /// <param name="policeCase"></param>
        /// <param name="pageHelper"></param>
        public ApproveController(IApprove approve, IPoliceCase policeCase, IPageHelper pageHelper)
        {
            _approve = approve;
            _policeCase = policeCase;
            _pageHelper = pageHelper;
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
        public IActionResult GetApproveList(int id=1)
        {
            pageParams.CurPage = id;
            pageParams.Filter += "  and Approve.RoleId=" + loginRoleId;
            pageParams.PageSize = pageSize;
            PageList<ApproveDataModel> pageList = _pageHelper.InfoList<ApproveDataModel>(pageParams);
            List<ApproveDataModel> list = pageList.ListData;
            return View(list);
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bussiness"></param>
        /// <returns></returns>
        public void PassApprove(int id, int bussiness, int inStanceId)
        {
            int loginRoleId = 2;
            int userID = 2;
            Approve approve = _approve.GetApproveById(id);
            ApproveCourse approveCourse = _approve.GetApproveCoursesList(approve.PlaceID);
            if (approveCourse != null)
            {
                if (approveCourse.PostpositionID == 0)
                {
                    approve.Ideas = "";
                    approve.State = "2";
                    approve.RoleId = loginRoleId;
                    approve.ApprovePeopleId = userID;
                    approve.Time = DateTime.Now;
                    int result = _approve.UpdateApprove(approve);
                    if (result > 0)
                    {
                        Instance instance = _policeCase.GetInstanceById(inStanceId);
                        instance.ApproveState = 2;
                        int i = _policeCase.UpdateinStance(instance);
                        if (i > 0)
                        {
                            Content("<script>alert('审核通过！')</script>");
                        }
                    }
                }
                else
                {
                    approve.Ideas = "";
                    approve.State = "1";
                    approve.RoleId = approveCourse.ApproveRoleId; 
                    approve.ApprovePeopleId = approveCourse.ApproveRoleId;
                    approve.Time = DateTime.Now;
                    approve.PlaceID = approveCourse.PostpositionID;
                    int result = _approve.UpdateApprove(approve);
                    if (result > 0)
                    {

                        Content("<script>alert('您的审核通过！正在进行下一级审核')</script>");
                        //PassApprove(approve.ID, approve.BusinesstypeId, inStanceId);
                    }

                }
            }
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inStanceId"></param>
        public void NoApprove(int id, int inStanceId)
        {
            Approve approve = _approve.GetApproveById(id);
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
        /// 绑定立案、结案下拉
        /// </summary>
        public void GetBusinesses()
        {
            List<Business> list= _policeCase.GetBusinessesList();
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