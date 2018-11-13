using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.Models.DTO;

namespace FPS.IServices
{
    /// <summary>
    /// 报警管理
    /// </summary>
    public interface IAlarm
    {


        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        int InsertAlarm(Alarm alarm);

        /// <summary>
        /// 操作报警内容
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        int UptAlarm(int id,Alarm alarm);

        /// <summary>
        /// 操作警员状态
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        int UptUserState(int userid);

        /// <summary>
        /// 警员归队操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int UptState(int id);

        /// <summary>
        /// 警员归队状态修改
        /// </summary>
        /// <param name="outid"></param>
        /// <returns></returns>
        int UptOverOperationState(int outid);
        /// <summary>
        /// 外派警员列表
        /// </summary>
        /// <returns></returns>
        List<UserAndRole> GetRoles(); 
    }
}
