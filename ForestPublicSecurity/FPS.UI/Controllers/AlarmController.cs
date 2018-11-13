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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

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

        #region ///Vue显示传值
        //public JsonResult GetAlarmLis()
        //{

        //    //当前第几页
        //    var pageIndex = 1;

        //    StringBuilder str = new StringBuilder();
        //    str.Append(" 1=1");
        //    //拼接字符串
        //    PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 5, Sort = "ID desc", TableName = "Alarm" };
        //    PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
        //    var alarmlist = pList.ListData;

        //    ViewBag.totalNumber = pList.TotalCount;
        //    return Json(alarmlist);
        //}
        #endregion
            
        /// <summary>
        /// 报警显示
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //使用公共方法
            PageList<Alarm> pList;
            List<Alarm> alarmlist;
            GetInfo(out pList, out alarmlist);

            //获取登录人信息
            //var seesi = HttpContext.Session.GetString("user");
            //var user = JsonConvert.DeserializeObject<UserAndRole>(seesi);

            //登录人权限名称
            //ViewBag.userName = user.RoleName;
            ViewBag.userName = "警员";
            //ViewBag.userName = "队长";
            ViewBag.totalNumber = pList.TotalCount;
            return View(alarmlist);
        }

        /// <summary>
        /// 查询显示
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
        public ActionResult Index(string name = "", string phone = "", string idcard = "", string detailSplace = "", string beginTime = "", string endTime = "", int pageIndex = 1)
        {
            var getInfoTerm= GetInfpTerm(name, phone, idcard, detailSplace, beginTime, endTime, pageIndex);
            return getInfoTerm;
        }
      
        /// <summary>
        /// 队长管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CaptainOperation()
        {
            //使用公共方法
            PageList<Alarm> pList;
            List<Alarm> alarmlist;
            GetInfo(out pList, out alarmlist);

            return View(alarmlist);
        }

        /// <summary>
        /// 警员操作管理
        /// </summary>
        /// <returns></returns>

        public ActionResult ConstableOperation()
        {
            //使用公共方法
            PageList<Alarm> pList;
            List<Alarm> alarmlist;
            GetInfo(out pList, out alarmlist);

            return View(alarmlist);
        }

        /// <summary>
        /// 外派警员页面
        /// </summary>
        /// <returns></returns>

        public ActionResult SolvePeople(int infoid)
        {
            HttpContext.Session.SetString("info", JsonConvert.SerializeObject(infoid));

            //外派警员的下拉列表
            ViewBag.id = new SelectList(_alarm.GetRoles(), "ID", "RoleName");

            return View();
        }

        /// <summary>
        /// 外派警员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SolvePeople1(int id)
        {
            var ids =Convert.ToInt32(JsonConvert.DeserializeObject(HttpContext.Session.GetString("info")));
            //获取登录人信息
            var seesi = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<UserAndRole>(seesi);
            
            var alarms = new Alarm()
            {
                OutID= id,
                SolvePeopleId=user.ID,
                OverTime = new DateTime()
            };
             var result = _alarm.UptAlarm(ids, alarms);
            return View();
        }

        /// <summary>
        /// 警员归队
        /// </summary>
        /// <returns></returns>
        public ActionResult OverOperation(int id)
        {
            var result = _alarm.UptState(id);
            if (result>0)
            {
                return Content("<script>alert('操作成功');location.href='/Alarm/ConstableOperation'</script>", "text/html;charset=utf-8");
            }
            return Content("<script>alert('操作成功');location.href='/Alarm/ConstableOperation'</script>", "text/html;charset=utf-8");

        }
        
        /// <summary>
        /// 显示公共方法
        /// </summary>
        /// <param name="pList"></param>
        /// <param name="alarmlist"></param>
        private void GetInfo(out PageList<Alarm> pList, out List<Alarm> alarmlist)
        {
            //当前第几页
            var pageIndex = 1;

            StringBuilder str = new StringBuilder();
            str.Append(" 1=1");
            //拼接字符串
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 5, Sort = "ID desc", TableName = "Alarm" };
            pList = _pageHelper.InfoList<Alarm>(pageParams);
            alarmlist = pList.ListData;
        }

        /// <summary>
        /// 查询的公共方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idcard"></param>
        /// <param name="detailSplace"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private ActionResult GetInfpTerm(string name, string phone, string idcard, string detailSplace, string beginTime, string endTime, int pageIndex)
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
                str.Append(" and PHONE='" + phone + "'");
            }
            if (!string.IsNullOrWhiteSpace(idcard))
            {
                str.Append(" and IDCARD='" + idcard + "'");
            }
            if (!string.IsNullOrWhiteSpace(detailSplace))
            {
                str.Append(" and DETAILSPLACE='" + detailSplace + "'");
            }
            if (!string.IsNullOrWhiteSpace(beginTime) && !string.IsNullOrWhiteSpace(endTime))
            {
                str.Append(" and TIME between '" + beginTime + "' end '" + endTime + "'");
            }

            //拼接语句
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 4, Sort = "ID desc", TableName = "Alarm" };
            PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
            var alarmlist = pList.ListData;
            return View(alarmlist);
        }
    }
}