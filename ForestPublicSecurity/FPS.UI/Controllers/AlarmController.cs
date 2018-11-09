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

namespace FPS.UI.Controllers
{
    public class AlarmController : Controller
    {
        private readonly IAlarm _alarm;
        
        public AlarmController(IAlarm alarm)
        {
            _alarm = alarm;
        }

        /// <summary>
        /// 报警显示
        /// </summary>
        /// <returns></returns>

        //public IActionResult Index(int pageindex=1)
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.Append(" 1=1");
        //    //拼接字符串
        //    PageParams param = new PageParams() { StrWhere =str.ToString() , TableName = "Student", Columns = "*",OrderCol = "ID desc", PageIndex = pageindex, PageSize = 5 };
        //    PageList<Alarm> alarmList = PageCommon.PagingCommon<Alarm>(param);

        //    return View(alarmList);
        //}

        public ActionResult Index()

        {
            var studenList = _alarm.GetStudents();
            return View(studenList);
            //StringBuilder str = new StringBuilder();
            //str.Append(" 1=1");
            ////拼接字符串
            //PageParams param = new PageParams() { StrWhere = str.ToString(), TableName = "Student", Orderby = "ID desc", Page = pageindex, PageSize = 5 };
            //PageList<Alarm> alarmList = PageCommon.PagingCommon<Alarm>(param);

            //return View(alarmList);
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