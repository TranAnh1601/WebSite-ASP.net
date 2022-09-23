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
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        // khai báo hàm csdl
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
       
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            //var lstCategory = objWebSiteBanHangEntities.Categories.ToList(); //sử dụng 1 bẩng cũ
            //var lstProduct = objWebSiteBanHangEntities.Products.ToList();
            // sử dụng model load 2 bảng csdl
            objHomeModel.ListCategory = objWebSiteBanHangEntities.Categories.Where(n=>n.ShowOnHomePage==true).ToList();
            objHomeModel.ListProduct = objWebSiteBanHangEntities.Products.Where(n => n.Deleted == false && n.ShowOnHomePage==true).ToList();
                return View(objHomeModel);
        }

        [HttpGet] // hiển thị đường dẫn
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost] // giúp ẩn đường dẫn
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user) // khai bao bien user
        {
            if (ModelState.IsValid)
            {
                var check = objWebSiteBanHangEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null) // nếu k trùng Email
                {
                    _user.Password = GetMD5(_user.Password); // mã hóa pass
                    objWebSiteBanHangEntities.Configuration.ValidateOnSaveEnabled = false;// sau khi mã hóa password
                    _user.IsAdmin = false;
                    objWebSiteBanHangEntities.Users.Add(_user);
                    objWebSiteBanHangEntities.SaveChanges();
                    ViewBag.error = "Đăng kí thành công";
                    return RedirectToAction("Login");

                }
                else
                {
                    ViewBag.error = "Email tồn tại!";
                    return View("Register");
                }
                
            }
            return View();

        }
        [HttpGet]
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            
            if (ModelState.IsValid)//nhận thông tin
            {
                var f_password = GetMD5(password);//mã hóa lại pass
                                                  //User user = objWebSiteBanHangEntities.Users.Where(n=>n.Email==email).FirstOrDefault();

                //if (user != null) // co tk
                //{
                //    if (user.Password.Equals(f_password))
                //    {
                //        //add session
                //        Session["FullName"] = user.FirstName+user.LastName;// lưu vào sesstion
                //        Session["Email"] = user.Email;
                //        Session["Password"] = user.Password;
                //        Session["idUser"] = user.Id;
                //        return RedirectToAction("Index");

                //    }
                //    else
                //    {

                //        ViewBag.Error = " <p class='login-box-msg text-danger'>Mật khẩu không đúng! </p>";
                //    }
                //}
                //else
                //{
                //    ViewBag.Error = " <p class='login-box-msg text-danger'>Tên đăng nhập <strong>" + email + " </strong>không tồn tại</p>";
                //}               





                var data = objWebSiteBanHangEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList(); //tạo data và so sánh data
                if (data.Count() > 0)
                {

                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName; // lưu vào sesstion
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["Address"] = data.FirstOrDefault().Address;
                    if (data.FirstOrDefault().IsAdmin == true)
                    {
                        return View("~/admin/home/index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Vui lòng nhập chính xác tài khoản hoặc mật khẩu";
                    return View();
                }
            }
            return View();
        }

    

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index","Home");
        }
        public ActionResult Search(string SearchString, int page = 1, int size = 16)
        {
            HomeModel objHomeModel = new HomeModel();
            ViewBag.Error = SearchString;
            if (SearchString!= null)
            {
                //objHomeModel.lstProduct = objWebSiteBanHangEntities.Products.Where(n => n.Deleted == false && n.ShowOnHomePage == true).OrderByDescending(n => n.Id).ToPagedList(page, size);
                var lstProduct = objWebSiteBanHangEntities.Products.Where(n => n.Name.Contains(SearchString) && n.Deleted == false && n.ShowOnHomePage == true).ToList();
                lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
                return View(lstProduct);
            }
            else
            {                 
                return View("Index");
            }
            
        }
        public ActionResult ProductSale()
        {
            var objProductSale = objWebSiteBanHangEntities.Products.Where(n => n.TypeId == 1 && n.Deleted == false && n.ShowOnHomePage==true).ToList();
            return View(objProductSale);
        }

    }
}