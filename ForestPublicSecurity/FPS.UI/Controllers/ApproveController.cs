using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;

namespace FPS.UI.Controllers
{
    public class ApproveController : Controller
    {
        private readonly IApprove _approve;

        public ApproveController(IApprove approve)
        {
            _approve = approve;
        }

        public IActionResult GetApproveList()
        {
            return View();
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bussiness"></param>
        /// <returns></returns>
        public void PassApprove(int id, int bussiness)
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
                    approve.State = "3";
                    approve.RoleId = loginRoleId;
                    approve.ApprovePeopleId = userID;
                    approve.Time = DateTime.Now;
                    int result = _approve.UpdateApprove(approve);
                    if (result>0)
                    {
                        Content("<script>alert('审核通过！')</script>");
                    }
                }
                else
                {
                    approve.Ideas = "";
                    approve.State = "1";
                    approve.RoleId = loginRoleId;
                    approve.ApprovePeopleId = approveCourse.ApproveRoleId;
                    approve.Time = DateTime.Now;
                    approve.PlaceID = approveCourse.PostpositionID;
                    int result = _approve.UpdateApprove(approve);
                    if (result>0)
                    {
                        
                        Content("<script>alert('您的审核通过！正在进行下一级审核')</script>");
                        PassApprove(approve.ID, approve.BusinesstypeId);
                    }
                    
                }
            }
        }
    }
}