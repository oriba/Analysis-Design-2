using Coupons.DAL;
using Coupons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Coupons.Controllers
{
    public abstract class BaseController : Controller
    {

        protected override ViewResult View(IView view, object model)
        {
         
            return base.View(view, model);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var permissions = GuestPermissions.Default;
            if (Request.IsAuthenticated)
            {
                using (var context = new CouponsContext())
                {
                    var username = User.Identity.GetUserName();

                    var user = context.Users.FirstOrDefault(u => u.UserName == username);

                    if (user != null)
                    {
                        permissions = user.Permissions;
                    }
                }
            }

            ViewBag.Permissions = permissions;
            base.OnActionExecuted(filterContext);
        }

    }
}