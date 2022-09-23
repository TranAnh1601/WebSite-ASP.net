using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
       
        public ActionResult Index()
        {
            if(Session ["idUser"]== null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["Cart"];
                Order objOrder = new Order();
                
                objOrder.Name = " Đơn Hàng " + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId= int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
               
                objWebSiteBanHangEntities.Orders.Add(objOrder);
                objWebSiteBanHangEntities.SaveChanges();
               
                int intOrderId = objOrder.Id;
                
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                User User = new User();

                foreach(var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quanity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    obj.Price = (double?)Session["totalPayment"];
                    obj.Address = (string)Session["Address"];
                    lstOrderDetail.Add(obj);
                }
                objWebSiteBanHangEntities.OrderDetails.AddRange(lstOrderDetail);
                objWebSiteBanHangEntities.SaveChanges();
                Session["Cart"]= null;
            }
            return View();
        }
       
    }

}