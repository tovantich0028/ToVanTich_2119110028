using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class CategoryMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên danh muc")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hình đại diện")]
        [Display(Name = "Hình đại diện")]
        public string Avartar { get; set; }
        [Display(Name = "Tên")]
        public string Sulg { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOnUtc { get; set; }
        [Display(Name = "Xóa Id")]
        public Nullable<bool> DeleteId { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
       
    }
}