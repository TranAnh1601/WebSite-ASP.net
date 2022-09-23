using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;

namespace WebSiteBanHang.Controllers
{
    public class BrandController : Controller
    {
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        // GET: Brand
        public ActionResult Index()
        {
            var lstBrand = objWebSiteBanHangEntities.Brands.ToList();
            return View(lstBrand);
            
        }
        public ActionResult ProductBrand(int Id)
        {
            //HomeModel objHomeModel = new HomeModel();


            //objHomeModel.ListCategory = objWebSiteBanHangEntities.Categories.ToList();
            //objHomeModel.ListBrand = objWebSiteBanHangEntities.Brands.ToList();
            //objHomeModel.ListProduct = objWebSiteBanHangEntities.Products.Where(n => n.CategoryId == Id).ToList();
            //ViewBag.error = objHomeModel.ListProduct.Count();
            //return View(objHomeModel);
            var lstProduct = objWebSiteBanHangEntities.Products.Where(n => n.BrandId == Id).ToList();
            return View(lstProduct);
        }
    }
}