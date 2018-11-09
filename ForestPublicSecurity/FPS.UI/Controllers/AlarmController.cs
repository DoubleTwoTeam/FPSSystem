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

        public IActionResult Index()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" 1=1");
            //拼接字符串
            PageParams pageParams = new PageParams() { CurPage = 1, Fields = "ID,Name,Sex", Filter = "", PageSize = 4, Sort = "ID desc", TableName = "student" };
            PageList<Alarm> alarmList = _pageHelper.InfoList<Alarm>(pageParams);

            return View(alarmList);
        }

        /// <summary>
        /// 添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="alarm"></param>
        /// <returns></returns>
        [HttpPost]
        public int Add(Alarm alarm)
        {
            return 1;
        }
    }
}