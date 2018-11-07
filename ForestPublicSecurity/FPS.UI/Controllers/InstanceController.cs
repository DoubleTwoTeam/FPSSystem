using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;

namespace FPS.UI.Controllers
{
    public class InstanceController : Controller
    {
        /// <summary>
        /// 依赖注入案件接口
        /// </summary>
        private readonly IPoliceCase _policeCase;
        

        /// <summary>
        /// 案件显示页面
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInstanceList()
        {
            return View();
        }

        /// <summary>
        /// 案件添加页面
        /// </summary>
        /// <returns></returns>
        public IActionResult InsertPoliceCase()
        {
            return View();
        }
        /// <summary>
        /// 案件添加
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertPoliceCase(Instance instance)
        {

            return View();
        }


        public int  InsertApprove(int id)
        {
            return 0;
        }
    }
}