﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FPS.IServices;
using FPS.Models;

namespace FPS.UI.Controllers
{
    public class ApproveController : Controller
    {
        private readonly IApprove _approve;

        public ApproveController(IApprove approve)
        {
            _approve = approve;
        }

        public IActionResult GetApproveList()
        {
            return View();
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bussiness"></param>
        /// <returns></returns>
        public int PassApprove(int id,int bussiness)
        {
            return 0;
        }
    }
}