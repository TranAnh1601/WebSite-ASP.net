using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Context;
using WebSiteBanHang.Models;
using static WebSiteBanHang.Common;

namespace WebSiteBanHang.Areas.Admin.Controllers
{


    public class OrderController : Controller
    {
        WebSiteBanHangEntities1 objWebSiteBanHangEntities = new WebSiteBanHangEntities1();
        // GET: Admin/Order
        //public ActionResult Index()
        //{
        ////    OrderModel objorder = new OrderModel();
        ////    objorder.ListOrder = objWebSiteBanHangEntities.Orders.ToList();
        ////    objorder.ListOrderDetail = objWebSiteBanHangEntities.OrderDetails.ToList();
        //    var lstOrder = objWebSiteBanHangEntities.Orders.ToList();
        //    return View(lstOrder);
        //}
        //public ActionResult OrderDetail(int id)
        //{
        //    Order order = new Order();

        //  //  var objDetail = objWebSiteBanHangEntities.OrderDetails.Where();
        //        //return View(objDetail);
        //}
        public ActionResult Index(string currentFilter, int? page, string SearchString = "")
        {
            var lstOrder = new List<Order>();

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                //lấy ds sản phẩm theo từ khóa tìm kiếm
                lstOrder = objWebSiteBanHangEntities.Orders.Where(n => n.Name.Contains(SearchString)).ToList();

            }
            else
            {
                //lấy all sản phẩm trong bảng brand
                lstOrder = objWebSiteBanHangEntities.Orders.ToList();

            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            lstOrder = lstOrder.OrderByDescending(n => n.Id).ToList();
            //this.LoadData();

            return View(lstOrder.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objOrder = objWebSiteBanHangEntities.OrderDetails.Where(n => n.OrderId == id).FirstOrDefault();
            return View(objOrder);
        }
        public class OrderType
        {
            public int Id { get; set; }

            public string Name { get; set; }
            public Nullable<int> Status { get; set; }
            

        }
        void LoadData()
        {
            Common objCommon = new Common();


            var lstSta = objWebSiteBanHangEntities.Orders.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();

            DataTable dtSta = converter.ToDataTable(lstSta);
            ViewBag.ListStatus = objCommon.ToSelectList(dtSta, "Id", "Name");

            //Trạng Thái đơn hàng
            List<OrderType> lstOrderType = new List<OrderType>();
            OrderType lstorderType = new OrderType();
            OrderType objoderType = new OrderType();
            objoderType.Id = 01;
            objoderType.Name = "Chờ Lấy Hàng";
            lstOrderType.Add(objoderType);

            objoderType = new OrderType();
            objoderType.Id = 02;
            objoderType.Name = "Đang Vận Chuyển";
            lstOrderType.Add(objoderType);

            objoderType = new OrderType();
            objoderType.Id = 03;
            objoderType.Name = "Giao Hàng Thành Công";
            lstOrderType.Add(objoderType);

            objoderType = new OrderType();
            objoderType.Id = 04;
            objoderType.Name = "Đã Hủy";
            lstOrderType.Add(objoderType);


            DataTable dtOrderType = converter.ToDataTable(lstOrderType);

            ViewBag.OrderType = objCommon.ToSelectList(dtOrderType, "Id", "Name");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objOrder = objWebSiteBanHangEntities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }


        [HttpPost]
        public ActionResult Edit(Order objOrder)
        {
            objWebSiteBanHangEntities.Entry(objOrder).State = EntityState.Modified;
            objWebSiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}