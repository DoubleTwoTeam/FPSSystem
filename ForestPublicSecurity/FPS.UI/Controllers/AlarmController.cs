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
            //当前第几页
            var pageIndex = 1;

            StringBuilder str = new StringBuilder();
            str.Append(" 1=1");
            //拼接字符串
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 5, Sort = "ID desc", TableName = "Alarm" };
            PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
            List<Alarm> alarmlist = pList.ListData;

            //获取登录人信息
            var seesi = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<UserAndRole>(seesi);

            var userid = user.ID;
            // 登录人权限名称
            ViewBag.userName = user.RoleName;
            ViewBag.totalNumber = pList.TotalCount;

            //将操作的列ID 取到存在Session中
            HttpContext.Session.SetString("userid", JsonConvert.SerializeObject(userid));
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
            //拼接条件
            StringBuilder str = new StringBuilder();
            str.Append("1=1");
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
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 5, Sort = "ID desc", TableName = "Alarm" };
            PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
            var alarmlist = pList.ListData;
            return View(alarmlist);
        }

        /// <summary>
        /// 队长管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CaptainOperation()
        {
            //当前第几页
            var pageIndex = 1;

            StringBuilder str = new StringBuilder();
            str.Append(" 1=1");
            //拼接字符串
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 5, Sort = "ID desc", TableName = "Alarm" };
            PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
            List<Alarm> alarmlist = pList.ListData;

            return View(alarmlist);
        }

        /// <summary>
        /// 警员操作管理
        /// </summary>
        /// <returns></returns>

        public ActionResult ConstableOperation()
        {
            var rid = Convert.ToInt32(JsonConvert.DeserializeObject(HttpContext.Session.GetString("userid")));
            //当前第几页
            var pageIndex = 1;

            StringBuilder str = new StringBuilder();
            str.Append(" OutID ='" + rid + "'");
            //拼接字符串
            PageParams pageParams = new PageParams() { CurPage = pageIndex, Fields = "ID,ALARMREASON,DETAILSPLACE,ENCLOSURE,TIME,ALARMPEOPLE,PHONE,IDCARD,URL,SOLVEPEOPLEID,OUTID,OVERTIME,STATE", Filter = str.ToString(), PageSize = 5, Sort = "ID desc", TableName = "Alarm" };
            PageList<Alarm> pList = _pageHelper.InfoList<Alarm>(pageParams);
            List<Alarm> alarmlist = pList.ListData;

            return View(alarmlist);
        }

        /// <summary>
        /// 外派警员页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SolvePeople(int id)
        {
            //将操作的列ID 取到存在Session中
            HttpContext.Session.SetString("info", JsonConvert.SerializeObject(id));
            //外派警员的下拉列表
            ViewBag.id = new SelectList(_alarm.GetRoles(), "ID", "RealName");
            return View();
        }

        /// <summary>
        /// 外派警员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UptSolvePeople(int id)
        {
            var ids = Convert.ToInt32(JsonConvert.DeserializeObject(HttpContext.Session.GetString("info")));
            //获取登录人信息
            var seesi = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<UserAndRole>(seesi);

            var alarms = new Alarm()
            {
                OutID = id,
                SolvePeopleId = user.ID,
                OverTime = DateTime.Now
            };
            //修改信息
            var result = _alarm.UptAlarm(ids, alarms);
            //修改警员状态
            var resultTwo = _alarm.UptUserState(id);
            if (result > 0)
            {
                if (resultTwo>0)
                {
                    return Content("<script>alert('操作成功');var index = parent.layer.getFrameIndex(window.name);window.parent.location.reload();parent.layer.close(index); location.href='/Alarm/CaptainOperation'</script>", "text/html;charset=utf-8");
                }
            }
            return Content("<script>alert('操作失败');var index = parent.layer.getFrameIndex(window.name);window.parent.location.reload();parent.layer.close(index); location.href='/Alarm/CaptainOperation'</script>", "text/html;charset=utf-8");
        }

        /// <summary>
        /// 警员归队
        /// </summary>
        /// <returns></returns>
        public ActionResult OverOperation(int id)
        {
            //获取登录人信息
            var seesi = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<UserAndRole>(seesi);

            var userid = user.ID;
            var result = _alarm.UptState(id);
            var resultTwo = _alarm.UptOverOperationState(userid);
            if (result > 0)
            {
                if (resultTwo>0)
                {
                    return Content("<script>alert('操作成功');location.href='/Alarm/ConstableOperation'</script>", "text/html;charset=utf-8");
                }
            }
            return Content("<script>alert('操作成功');location.href='/Alarm/ConstableOperation'</script>", "text/html;charset=utf-8");
        }
    }
}