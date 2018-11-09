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

        List<Alarm> GetAlarms();

        /// <summary>
        /// 显示报警内容
        /// </summary>
        /// <returns></returns>
        PageList<T> PagingCommon<T>(PageParams pageparams);

        /// <summary>
        /// 反填显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable GetUptAlarm(int id);
        /// <summary>
        /// 操作报警内容
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        int UptAlarm(Alarm alarm);

    }
}
