using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public partial class UserMasterData
    {
        public int Id { get; set; }

        [Display(Name = "Họ")]
        public string FirstName { get; set; }
        [Display(Name = "Tên")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tài khoản")]
        [Display(Name = "Tài khoản")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
    }
}