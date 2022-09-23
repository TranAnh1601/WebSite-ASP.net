using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        Common common = new Common();
        public ActionResult Index(string SearchString, string curentFilter, int? page)
        {
            // this.LoadData();
            
            var lstUser = new List<User>();
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
                lstUser = objWebSiteBanHangEntities.Users.Where(n => n.FirstName.Contains(SearchString) && n.LastName.Contains(SearchString)).ToList();
            }
            else
            {
                lstUser = objWebSiteBanHangEntities.Users.ToList();
            }

            ViewBag.curentFilter = SearchString;
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            //sp moi len dau
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int id)
        {
            var objUser = objWebSiteBanHangEntities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
       
        public ActionResult Delete(User objU)
        {
            var objUser = objWebSiteBanHangEntities.Users.Where(n => n.Id == objU.Id).FirstOrDefault();
            objWebSiteBanHangEntities.Users.Remove(objUser);
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            var objUser = objWebSiteBanHangEntities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Edit(User objU)
        {

                    objU.Password = GetMD5(objU.Password); // mã hóa pass
                    objWebSiteBanHangEntities.Configuration.ValidateOnSaveEnabled = false;// sau khi mã hóa password
                    objU.IsAdmin = false;
                    objWebSiteBanHangEntities.Entry(objU).State = EntityState.Modified; //Modified : chinh sua, EntityState: thu vien
                    objWebSiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");          
        }
    }
   


}