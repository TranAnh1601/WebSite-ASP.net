using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSiteBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
         
            routes.MapRoute(
                  name: "Login",
                  url: "dang-nhap",
                  defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional },
                      new[] { "WebSiteBanHang.Controllers" }
             );
            routes.MapRoute(
                  name: "ProductSale",
                  url: "san-pham-giam-gia",
                  defaults: new { controller = "Home", action = "ProductSale", id = UrlParameter.Optional },
                      new[] { "WebSiteBanHang.Controllers" }
             );
            routes.MapRoute(
                  name: "Home",
                  url: "trang-chu",
                  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                      new[] { "WebSiteBanHang.Controllers" }
             );
            routes.MapRoute(
                  name: "Cart",
                  url: "gio-hang",
                  defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                      new[] { "WebSiteBanHang.Controllers" }
             );
            routes.MapRoute(
                  name: "Register",
                  url: "dang-ky",
                  defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional },
                      new[] { "WebSiteBanHang.Controllers" }
             );
            routes.MapRoute(
                 name: "Search",
                 url: "tim-kiem",
                 defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional },
                     new[] { "WebSiteBanHang.Controllers" }
            );
            routes.MapRoute(
                name: "Category",
                url: "danh-muc",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
                    new[] { "WebSiteBanHang.Controllers" }
           );
            routes.MapRoute(
               name: "Product",
               url: "san-pham/{Slug}/{Id}",
               defaults: new { Controller = "Product", action = "Detail", id = UrlParameter.Optional },
               new[] { "WebSiteBanHang.Controllers" }
           );

            routes.MapRoute(
               name: "ProductCategory",
               url: "danh-muc/{Slug}/{Id}",
               defaults: new { Controller = "Category", action = "ProductCategory", id = UrlParameter.Optional },
               new[] { "WebSiteBanHang.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebSiteBanHang.Controllers" }
            );
           
        }
    }
}
