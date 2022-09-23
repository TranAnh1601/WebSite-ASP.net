using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;

namespace WebSiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebSiteBanHangEntities.Products.Where(n=> n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }

        
    }
}