using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public partial class ProductMasterData
    {
        // them partial class de rang buoc du lieu khi su dung data first
        public int Id { get; set; }
     
        [Display(Name ="Tên Sản Phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không để trống!")]
        public string Name { get; set; }
        [Display(Name = "Hình Sản Phẩm")]
      
        public string Avatar { get; set; }
        [Display(Name = "Danh mục Sản Phẩm" )]
        public Nullable<int> CategoryId { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string ShortDes { get; set; }
        [Display(Name = "Mô tả đầy đủ")]
        public string FullDescription { get; set; }
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá không để trống!")]
        public Nullable<double> Price { get; set; }
        [Display(Name = "Giá Sale")]
        public Nullable<double> PriceDiscount { get; set; }
        [Display(Name = "Kiểu")]
        public Nullable<int> TypeId { get; set; }
        [Display(Name = "Từ khóa")]
        
        public string Slug { get; set; }
        [Display(Name = "Thương hiệu")]
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Ngày Tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Ngày sửa")]
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }

    }
}