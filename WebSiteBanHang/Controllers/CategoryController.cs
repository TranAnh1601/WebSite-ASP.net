using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objWebSiteBanHangEntities.Categories.Where(n=>n.ShowOnHomePage==true).ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id, int page=1, int size=8)
        {
            
            HomeModel objHomeModel = new HomeModel();          
            objHomeModel.ListCategory = objWebSiteBanHangEntities.Categories.ToList();
            objHomeModel.ListBrand = objWebSiteBanHangEntities.Brands.ToList();
            objHomeModel.lstProduct = objWebSiteBanHangEntities.Products.
                Where(n => n.CategoryId == Id && n.Deleted==false && n.ShowOnHomePage==true).OrderByDescending(n=>n.Id).ToPagedList(page,size);
         
            return View(objHomeModel);
            //var lstProduct = objWebSiteBanHangEntities.Products.Where(n => n.CategoryId == Id).ToList();          
            //return View (lstProduct);
        }
    }
}