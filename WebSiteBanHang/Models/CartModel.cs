using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Context;

namespace WebSiteBanHang.Models
{
    public class CartModel
    {
        public Product Product { get; set; } // doi tuong san  pham, thong tin sp

        public int Quantity { get; set; }

        public User User { get; set; }
    }
}