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
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        // khai báo hàm csdl
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            User user = new User();
            ViewBag.error = "";
            if (ModelState.IsValid)//nhận thông tin
            {
                var f_password = GetMD5(password);//mã hóa lại pass
                var data = objWebSiteBanHangEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList(); //tạo data và so sánh data
                if (data.Count() > 0 && user.IsAdmin == true)
                {
                    //add session
                    Session["UserAdmin"] = user.Id;                     
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login", "Auth");
                }
            }
            return View();
        }

        //public ActionResult DoLogin(FormCollection field)
        //{
        //    ViewBag.Error = "";
        //    string username = field["username"];
        //    string password = field["password"];
        //    //kiem tra trong csdl co user khong? admin
        //    User user = objWebSiteBanHangEntities.Users
        //        .Where(m => m.IsAdmin == true && m.Email == username).FirstOrDefault();
        //    var data = objWebSiteBanHangEntities.Users.Where(s => s.Email.Equals(username) && s.Password.Equals(password)).ToList(); //tạo data và so sánh data
        //    if (user.Email != null) // co tk
        //    {
        //        if (user.IsAdmin != true)
        //        {
        //            ViewBag.Error = " <p class='login-box-msg text-danger'> Vui lòng liên hệ lại </p>";
        //        }
        //        else
        //        {
        //            if (user.Password.Equals(password))
        //            {
        //                //khớp
        //                Session["UserAdmin"] = username; // null
        //                Session["UserId"] = user.Id.ToString();
        //                Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName; // lưu vào sesstion                
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                objWebSiteBanHangEntities.Entry(user).State = EntityState.Modified;
        //                objWebSiteBanHangEntities.SaveChanges();
        //                ViewBag.Error = " <p class='login-box-msg text-danger'>Mật khẩu không đúng! </p>";
        //            }
        //        }

        //        //hoi matkhau co khop khong?

        //    }
        //    else
        //    {
        //        ViewBag.Error = " <p class='login-box-msg text-danger'>Tên đăng nhập <strong>" + username + " </strong>không tồn tại</p>";
        //    }
        //    //select*form users Where
        //    return View("Login");
        //}
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login", "Auth");
        }
    }
}