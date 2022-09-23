using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Context;

namespace WebSiteBanHang.Models
{
    public class HomeModel
    {
        // tạo model chung
        public List<Product> ListProduct { get; set; } //khai báo list
        public List<Category> ListCategory { get; set; }

        public List<Brand> ListBrand { get; set; }

        public IPagedList<Product> lstProduct { get; set; }
    }
}