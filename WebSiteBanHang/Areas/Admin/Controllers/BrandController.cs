using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller

    {
        //BrandMasterData objWebSiteBanHangEntities = new BrandMasterData();      
            WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1(); 
        // GET: Admin/Brand
        public ActionResult Index(string SearchString, string curentFilter, int? page)
        {
           // this.LoadData();
            var lstBrand = new List<Brand>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = curentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Name.Contains(SearchString) && n.Deleted == false).ToList();
            }
            else
            {
                lstBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Deleted == false).ToList();
            }
            ViewBag.curentFilter = SearchString;
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            //sp moi len dau
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]

        public ActionResult Create()
        {
           // this.LoadData();
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
           // this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName); // lay ten hinh
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);// phan mo rong nhu png/pejg
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension; // them thpi gian de tranh trung ten hinh
                        objBrand.Avatar = fileName; // gan du lieu vao avata
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    objBrand.Deleted = false;
                    objBrand.ShowOnHomePage = true;
                    objWebSiteBanHangEntities.Brands.Add(objBrand);
                    objWebSiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");// luu thanh cong tra ve trang index
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return RedirectToAction("objBrand");

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand= objWebSiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBra)
        {
            var objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Id == objBra.Id).FirstOrDefault();
            objWebSiteBanHangEntities.Brands.Remove(objBrand);
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Trash");
        }
        public ActionResult Edit(int id)
        {
           
            var objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Edit(Brand objBrand)
        {
          
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName); // lay ten hinh
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);// phan mo rong nhu png/pejg
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension; // them thpi gian de tranh trung ten hinh
                objBrand.Avatar = fileName; // gan du lieu vao avata
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objBrand.UpdatedOnUtc = DateTime.Now;
            objWebSiteBanHangEntities.Entry(objBrand).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
      
       
        public ActionResult Trash(string SearchString, string curentFilter, int? page)
        {
            var objBrand = new List<Brand>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = curentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Name.Contains(SearchString) && n.Deleted == true).ToList();
            }
            else
            {
                objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Deleted == true).ToList();
            }
            ViewBag.curentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            //sp moi len dau
            objBrand = objBrand.OrderByDescending(n => n.Id).ToList();
            return View(objBrand.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReTrash(int? id)
        {

            var objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            objBrand.Deleted = false;
            objWebSiteBanHangEntities.Entry(objBrand).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DelTrash(int? id)
        {
            var objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            objBrand.Deleted = true;
            objWebSiteBanHangEntities.Entry(objBrand).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ShowHomePage(int id)
        {
            var objBrand = objWebSiteBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            objBrand.ShowOnHomePage = (objBrand.ShowOnHomePage == true) ? false : true;
            objWebSiteBanHangEntities.Entry(objBrand).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
       
     
    }
}