using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;
using WebSiteBanHang.Models;
using static WebSiteBanHang.Common;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        Common common = new Common();
        // GET: Admin/Product
        public ActionResult Index(string SearchString, string curentFilter, int? page)
        {
            this.LoadData();
            var lstProduct = new List<Product>();
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
                lstProduct = objWebSiteBanHangEntities.Products.Where(n => n.Name.Contains(SearchString) && n.Deleted ==  false).ToList();
            }
            else
            {
                lstProduct = objWebSiteBanHangEntities.Products.Where(n=>n.Deleted==false).ToList();
            }
            ViewBag.curentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            //sp moi len dau
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]

        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName); // lay ten hinh
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);// phan mo rong nhu png/pejg
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension; // them thpi gian de tranh trung ten hinh
                        objProduct.Avatar = fileName; // gan du lieu vao avata
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objProduct.Deleted = false;
                    objProduct.ShowOnHomePage = true;
                    common.Init_SlugName(objProduct.Name);
                    
                    objProduct.Slug = common.Init_SlugName(objProduct.Name);
                    objWebSiteBanHangEntities.Products.Add(objProduct);
                    objWebSiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");// luu thanh cong tra ve trang index
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return  View();

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWebSiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebSiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWebSiteBanHangEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebSiteBanHangEntities.Products.Remove(objProduct);
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objWebSiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            this.LoadData();
            if(objProduct.Name != null)
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName); // lay ten hinh
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);// phan mo rong nhu png/pejg
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension; // them thpi gian de tranh trung ten hinh
                    objProduct.Avatar = fileName; // gan du lieu vao avata
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objProduct.UpdateOnUtc = DateTime.Now;

                common.Init_SlugName(objProduct.Name);
                objProduct.Slug = common.Init_SlugName(objProduct.Name);
                objWebSiteBanHangEntities.Entry(objProduct).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
                objWebSiteBanHangEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(objProduct);
            }
            
        }
        public class ProductType
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }
        void LoadData()
        {
            Common objCommom = new Common();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();

            var lstCat = objWebSiteBanHangEntities.Categories.ToList();
            DataTable dtCategory = converter.ToDataTable(lstCat);

            ViewBag.ListCategory = objCommom.ToSelectList(dtCategory, "Id", "Name");// chuyen qua selec list
          

            var lstBrand = objWebSiteBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = objCommom.ToSelectList(dtBrand, "Id", "Name");

            List<ProductType> lstProductType = new List<ProductType>();

            ProductType objProductTyoe = new ProductType();
            objProductTyoe.Id = 01;
            objProductTyoe.Name = "Giảm giá sốc";
            lstProductType.Add(objProductTyoe);

            objProductTyoe = new ProductType();
            objProductTyoe.Id = 02;
            objProductTyoe.Name = "Đề xuất";
            lstProductType.Add(objProductTyoe);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommom.ToSelectList(dtProductType, "Id", "Name");
        }
        public ActionResult Trash(string SearchString, string curentFilter, int? page)
        {
            var lstProduct = new List<Product>();
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
                lstProduct = objWebSiteBanHangEntities.Products.Where(n => n.Name.Contains(SearchString) && n.Deleted == true).ToList();
            }
            else
            {
                lstProduct = objWebSiteBanHangEntities.Products.Where(n => n.Deleted == true).ToList();
            }
            ViewBag.curentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            //sp moi len dau
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReTrash(int? id)
        {

            var objProduct = objWebSiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            objProduct.Deleted = false;
            objWebSiteBanHangEntities.Entry(objProduct).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
          
        }
        public ActionResult DelTrash(int? id)
        {
            var objProduct = objWebSiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            objProduct.Deleted = true;
            objWebSiteBanHangEntities.Entry(objProduct).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ShowHomePage(int id)
        {
            var objProduct = objWebSiteBanHangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            objProduct.ShowOnHomePage = (objProduct.ShowOnHomePage == true) ? false : true;
            objWebSiteBanHangEntities.Entry(objProduct).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}