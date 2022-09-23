using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;

namespace WebSiteBanHang.Areas.Admin.Controllers
{

    public class BaseController : Controller
    {
        //GET: Admin/Base
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        //public BaseController()
        //{
        //    if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals("")) //kiem tra dang nhap neu = null chuyen huong web
        //    {
        //        System.Web.HttpContext.Current.Response.Redirect("~/Admin/Auth/Login");
        //    }
        //}
    }
}