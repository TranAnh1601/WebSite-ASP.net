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
    public class CategoryController : Controller
    {
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        // GET: Admin/Category
        public ActionResult Index(string SearchString, string curentFilter, int? page)
        {
            // this.LoadData();
            var lstCategory = new List<Category>();
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
                lstCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Name.Contains(SearchString) && n.Deleted == false).ToList();
            }
            else
            {
                lstCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Deleted == false).ToList();
            }
            ViewBag.curentFilter = SearchString;
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            //sp moi len dau
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]

        public ActionResult Create()
        {
            // this.LoadData();
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            // this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName); // lay ten hinh
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);// phan mo rong nhu png/pejg
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension; // them thpi gian de tranh trung ten hinh
                        objCategory.Avatar = fileName; // gan du lieu vao avata
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objCategory.Deleted = false;
                    objCategory.ShowOnHomePage = true;
                    objWebSiteBanHangEntities.Categories.Add(objCategory);
                    objWebSiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");// luu thanh cong tra ve trang index
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return RedirectToAction("objCategory");

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category obCate)
        {
            var objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Id == obCate.Id).FirstOrDefault();
            objWebSiteBanHangEntities.Categories.Remove(objCategory);
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Trash");
        }
        public ActionResult Edit(int id)
        {

            var objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {

            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName); // lay ten hinh
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);// phan mo rong nhu png/pejg
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension; // them thpi gian de tranh trung ten hinh
                objCategory.Avatar = fileName; // gan du lieu vao avata
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objCategory.UpdatedOnUtc = DateTime.Now;
            objWebSiteBanHangEntities.Entry(objCategory).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Trash(string SearchString, string curentFilter, int? page)
        {
            var objCategory = new List<Category>();
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
                objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Name.Contains(SearchString) && n.Deleted == true).ToList();
            }
            else
            {
                objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Deleted == true).ToList();
            }
            ViewBag.curentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            //sp moi len dau
            objCategory = objCategory.OrderByDescending(n => n.Id).ToList();
            return View(objCategory.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReTrash(int? id)
        {

            var objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            objCategory.Deleted = false;
            objWebSiteBanHangEntities.Entry(objCategory).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DelTrash(int? id)
        {
            var objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            objCategory.Deleted = true;
            objWebSiteBanHangEntities.Entry(objCategory).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ShowHomePage(int id)
        {
            var objCategory = objWebSiteBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            objCategory.ShowOnHomePage = (objCategory.ShowOnHomePage == true) ? false : true;
            objWebSiteBanHangEntities.Entry(objCategory).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}