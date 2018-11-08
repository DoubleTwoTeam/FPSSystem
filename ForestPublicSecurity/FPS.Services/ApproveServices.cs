using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.IServices;
using FPS.Models;
using Newtonsoft.Json;

namespace FPS.Services
{
    public class ApproveServices : IApprove
    {
        /// <summary>
        /// 审批页面显示
        /// </summary>
        /// <returns></returns>
        public List<ApproveDataModel> GetApproveList()
        {
            var db = SugerBase.GetInstance();
            List<ApproveDataModel> list= JsonConvert.DeserializeObject<List<ApproveDataModel>>(JsonConvert.SerializeObject(db.SqlQueryable<ApproveDataModel>(
                "select Approve.ID,Instance.ID as InstanceID,Instance.RegisterPeopleID,Business.ID as BusinessID,Business.Name as BusinessName,Users.realName as UsersName,Role.Name as RoleName,Approve.STATE,APPROVE.TIME " +
                "FROM Approve,Users,Instance,Business,Role " +
                "where Approve.ORIGINALID=Instance.ID and Approve.BUSINESSTYPEID=Business.ID and Approve.APPROVEPEOPLEID=USERS.ID and Approve.ROLEID=Role.ID")));
            return list;
        }

        /// <summary>
        /// 审批页面点击查看案件详情页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InstanceDataModel GetInstanceById(int id)
        {
            var db = SugerBase.GetInstance();
            InstanceDataModel inStance = JsonConvert.DeserializeObject<InstanceDataModel>(JsonConvert.SerializeObject(db.SqlQueryable<InstanceDataModel>(
                "select Instance.ID,Alarm.AlarmReason,Alarm.DetailSplace,Users.RealName,Instance.InstanceTypes,Instance.ApproveState,Instance.InstanceState,Instance.Time as InstanceTime " +
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

        public int UpdateApprove(Approve approve)
        {
            throw new NotImplementedException();
        }
    }
}
