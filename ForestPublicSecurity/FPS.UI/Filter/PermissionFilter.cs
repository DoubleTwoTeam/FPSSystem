using FPS.Models;
using FPS.UI.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPS.UI.Filter
{
    public class PermissionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取路径
            var path = filterContext.HttpContext.Request.Path.ToString();
            var user = filterContext.HttpContext.User;
            //Redis取值
            var userAndRole = RedisHelper.Get<UserAndRole>(user.Claims.ToList().Where(m=>m.Type=="user").First().Value);
            if (user.Claims.Count() > 0)
            {
                //验证权限
                var result = user.Claims.Where(m => m.Type == "keykey").First().Value;
                if (result != "qqq")
                {
                    filterContext.Result = new RedirectResult("/Center/Index");
                    base.OnActionExecuting(filterContext);
                    return;
                }
            }
            filterContext.Result = new RedirectResult("/Center/Index");
            base.OnActionExecuting(filterContext);
        }
    }
}
