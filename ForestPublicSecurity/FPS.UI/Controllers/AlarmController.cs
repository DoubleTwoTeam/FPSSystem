using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Services;
using FPS.Models;
using System.Text;
using FPS.Models.DTO;
using FPS.UI.Common;

namespace FPS.UI.Controllers
{
    public class AlarmController : Controller
    {
        private readonly IAlarm _alarm;
        private IPageHelper _pageHelper;

        public AlarmController(IAlarm alarm, IPageHelper pageHelper)
        {
            _alarm = alarm;
            _pageHelper = pageHelper;
        }

        /// <summary>
        /// 报警显示
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int pageIndex=1)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" 1=1");
            //拼接字符串
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter =str.ToString(), PageSize = 4, Sort = "ID desc", TableName = "Alarm" };
            PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
            var alarmlist = pList.ListData;
            
            ViewBag.totalNumber = pList.TotalCount;
            return View(alarmlist);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcard"></param>
        /// <param name="detailSplace"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string name="",string phone="",string idcard="",string detailSplace = "",string beginTime="",string endTime="",int pageIndex = 1)
        {
            //拼接条件
            StringBuilder str = new StringBuilder();
            str.Append("");
            if (!string.IsNullOrWhiteSpace(name))
            {
                str.Append(" and ALARMPEOPLE='" + name + "'");
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                str.Append(" and PHONE='"+phone+"'");
            }
            if (!string.IsNullOrWhiteSpace(idcard))
            {
                str.Append(" and IDCARD='" + idcard + "'");
            }
            if (!string.IsNullOrWhiteSpace(detailSplace))
            {
                str.Append(" and DETAILSPLACE='" + detailSplace + "'");
            }
            if (!string.IsNullOrWhiteSpace(beginTime)&&!string.IsNullOrWhiteSpace(endTime))
            {
                str.Append(" and TIME between '" + beginTime + "' end '" + endTime + "'");
            }

            //拼接语句
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 4, Sort = "ID desc", TableName = "Alarm" };
            PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
            var alarmlist = pList.ListData;

            return View();
        }
    }
}