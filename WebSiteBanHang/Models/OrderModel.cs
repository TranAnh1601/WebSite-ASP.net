using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Context;

namespace WebSiteBanHang.Models
{
    public class OrderModel
    {
        public List<Order> ListOrder { get; set; }
        public List<OrderDetail> ListOrderDetail { get; set; }
    }
}