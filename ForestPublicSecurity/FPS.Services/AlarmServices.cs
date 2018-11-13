using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.IServices;
using FPS.Models;
using FPS.Models.DTO;
using Newtonsoft.Json;

namespace FPS.Services
{
    public class AlarmServices : IAlarm
    {
        /// <summary>
        /// 外派警员列表
        /// </summary>
        /// <returns></returns>
        public List<UserAndRole> GetRoles()
        {
            var db = SugerBase.GetInstance();
            var userList = db.SqlQueryable<UserAndRole>("select B.ID,B.RealName,B.state from Role a,Users b,UserRole c where a.ID=c.roleid AND b.id=c.UserID AND C.ROLEID=88 and b.State=0").ToList();
            return userList;
        }

        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        public int InsertAlarm(Alarm alarm)
        {
            var db = SugerBase.GetInstance();
            var insertObj = new Alarm()
            {
                AlarmReason = alarm.AlarmReason,
                DetailSplace = alarm.DetailSplace,
                Enclosure = alarm.Enclosure,
                Time = alarm.Time,
                AlarmPeople = alarm.AlarmPeople,
                Phone = alarm.Phone,
                IdCard = alarm.IdCard,
                Url = alarm.Url
            };

            int ruselt = db.Insertable(insertObj).ExecuteCommand();
            return ruselt;
        }


        /// <summary>
        /// 操作报警信息
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        public int UptAlarm(int id, Alarm alarm)
        {
            var db = SimpleClientBase.GetSimpleClient<Alarm>();
            var result = db.Update(m => new Alarm {  OutID = alarm.OutID,SolvePeopleId=alarm.SolvePeopleId,OverTime=alarm.OverTime,State=1 }, q => q.ID == id) ? 1 : 0;
            return result;
        }

        /// <summary>
        /// 警员归队
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UptState(int id)
        {
            var db = SimpleClientBase.GetSimpleClient<Alarm>();
            var result = db.Update(m =>new Alarm { State=2 },q=>q.ID==id)?1:0;
            return result;
        }

        /// <summary>
        /// 警员状态修改
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UptUserState(int userid)
        {
            var db = SimpleClientBase.GetSimpleClient<Users>();
            var result =db.Update(n => new Users { State = 1 }, w => w.ID == userid) ? 1 : 0;
            return result;
        }

        /// <summary>
        /// 归队状态审批
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UptOverOperationState(int userid)
        {
            var db = SimpleClientBase.GetSimpleClient<Users>();
            var result = db.Update(n => new Users { State = 0 }, w => w.ID == userid) ? 1 : 0;
            return result;
        }
    }
}
