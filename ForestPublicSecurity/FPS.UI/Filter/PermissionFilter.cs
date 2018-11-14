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
            //var userAndRole = RedisHelper.Get<UserAndRole>(user.Identity.Name);
            var list = RedisHelper.Get<List<Authority>>(user.Identity.Name);
            //var flag = user.Identity.IsAuthenticated;
            if (user.Identity.IsAuthenticated)
            {
                //验证权限
                var result = list.Find(m => m.Url == path);
                if (result != null)  return;
            }
            filterContext.Result = new RedirectResult("/Center/Index");
            base.OnActionExecuting(filterContext);
        }
    }
}
