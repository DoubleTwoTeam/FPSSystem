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
        

        public IActionResult GetInstanceList()
        {
            return View();
        }

        public IActionResult InsertPoliceCase()
        {
            return View();
        }
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