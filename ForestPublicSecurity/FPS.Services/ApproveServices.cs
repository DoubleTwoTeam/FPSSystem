using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.IServices;
using FPS.Models;
using FPS.Models.DTO;
using Newtonsoft.Json;

namespace FPS.Services
{
    public class ApproveServices : IApprove
    {
        /// <summary>
        /// 根据ID返回审批对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Approve GetApproveById(int id)
        {
            var db = SugerBase.GetInstance();
            Approve approve = JsonConvert.DeserializeObject<Approve>(JsonConvert.SerializeObject(db.SqlQueryable<Approve>("select * from Approve where ID=" + id)));
            return approve;
        }

        /// <summary>
        /// 返回审批配置对象
        /// </summary>
        /// <param name="placed"></param>
        /// <returns></returns>
        public ApproveCourse GetApproveCoursesList(int placed)
        {
            var db = SugerBase.GetInstance();
            ApproveCourse approveCourse = db.Queryable<ApproveCourse>().Where(m=>m.ID==placed).Single();
            return approveCourse;
        }

        /// <summary>
        /// 审批页面显示
        /// </summary>
        /// <returns></returns>
        //PageList<ApproveDataModel> GetApproveList(PageParams pageParams)
        //{
        //    var db = SugerBase.GetInstance();
        //    List<ApproveDataModel> list = db.SqlQueryable<ApproveDataModel>(
        //        "select Approve.ID,Instance.ID as InstanceID,Instance.RegisterPeopleID,Business.ID as BusinessID,Business.Name as BusinessName,Users.realName as UsersName,Role.Name as RoleName,Instance.InstanceTypes,Instance.InstanceTime,Instance.ApproveState " +
        //        "from Approve,Users,Instance,Business,Role " +
        //        "where Approve.ORIGINALID=Instance.ID and Approve.BUSINESSTYPEID=Business.ID and Approve.APPROVEPEOPLEID=USERS.ID and Approve.ROLEID=Role.ID and Approve.State=1 "+pageParams.Filter).Skip(pageParams.CurPage).Take(pageParams.PageSize).ToList();

        //    int count = db.SqlQueryable<ApproveDataModel>(
        //        "select Approve.ID,Instance.ID as InstanceID,Instance.RegisterPeopleID,Business.ID as BusinessID,Business.Name as BusinessName,Users.realName as UsersName,Role.Name as RoleName,Instance.InstanceTypes,Instance.InstanceTime,Instance.ApproveState " +
        //        "from Approve,Users,Instance,Business,Role " +
        //        "where Approve.ORIGINALID=Instance.ID and Approve.BUSINESSTYPEID=Business.ID and Approve.APPROVEPEOPLEID=USERS.ID and Approve.ROLEID=Role.ID and Approve.State=1 " + pageParams.Filter).Count();

        //    PageList<ApproveDataModel> pagelist = new PageList<ApproveDataModel>() { ListData = list, TotalCount = count };

        //    return pagelist;
        //}


        /// <summary>
        /// 审批页面点击查看案件详情页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InstanceDataModel GetInstanceById(int id)
        {
            var db = SugerBase.GetInstance();
            InstanceDataModel inStance = JsonConvert.DeserializeObject<InstanceDataModel>(JsonConvert.SerializeObject(db.SqlQueryable<InstanceDataModel>(
                "select Instance.ID,Alarm.AlarmReason,Alarm.DetailSplace,Users.RealName,Instance.InstanceTypes,Instance.ApproveState,Instance.InstanceState,Instance.Time as InstanceTime,Instance.Sp " +
                "from Instance,Alarm,Users"+
                " where Instance.AlterID=Alarm.ID and Instance.RegisterPeopleID=Users.ID and Instance.ID="+id
                )));
            return inStance;
        }

        /// <summary>
        /// 案件添加到审批
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        public int InsertApprove(Approve approve)
        {
            var db = SugerBase.GetInstance();
            int result= db.Insertable(approve).ExecuteCommand();
            return result;
        }

        /// <summary>
        /// 修改审批对象
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        public int UpdateApprove(Approve approve)
        {
            var db = SugerBase.GetInstance();
            int result= db.Updateable<Approve>(approve).ExecuteCommand();
            return result;
        }

        /// <summary>
        /// 审批页面显示
        /// </summary>
        /// <returns></returns>
        PageList<ApproveDataModel> IApprove.GetApproveList(PageParams pageParams)
        {
            var db = SugerBase.GetInstance();
            List<ApproveDataModel> list = db.SqlQueryable<ApproveDataModel>(
                "select Approve.ID,Instance.ID as InstanceID,Instance.RegisterPeopleID,Business.ID as BusinessID,Business.Name as BusinessName,Users.realName as UsersName,Role.Name as RoleName,Instance.InstanceTypes,Instance.InstanceTime,Instance.ApproveState " +
                "from Approve,Users,Instance,Business,Role " +
                "where Approve.ORIGINALID=Instance.ID and Approve.BUSINESSTYPEID=Business.ID and Approve.APPROVEPEOPLEID=USERS.ID and Approve.ROLEID=Role.ID and Approve.State=1 " + pageParams.Filter).Skip(pageParams.CurPage).Take(pageParams.PageSize).ToList();

            int count = db.SqlQueryable<ApproveDataModel>(
                "select Approve.ID,Instance.ID as InstanceID,Instance.RegisterPeopleID,Business.ID as BusinessID,Business.Name as BusinessName,Users.realName as UsersName,Role.Name as RoleName,Instance.InstanceTypes,Instance.InstanceTime,Instance.ApproveState " +
                "from Approve,Users,Instance,Business,Role " +
                "where Approve.ORIGINALID=Instance.ID and Approve.BUSINESSTYPEID=Business.ID and Approve.APPROVEPEOPLEID=USERS.ID and Approve.ROLEID=Role.ID and Approve.State=1 " + pageParams.Filter).Count();

            PageList<ApproveDataModel> pagelist = new PageList<ApproveDataModel>() { ListData = list, TotalCount = count };

            return pagelist;
        }
    }
}
