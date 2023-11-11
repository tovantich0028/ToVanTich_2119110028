using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class BrandMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên thương hiệu")]
        [Display(Name = "Tên thương hiệu")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hình đại diện")]
        [Display(Name = "Hình đại diện")]
        public string Avartar { get; set; }
        [Display(Name = "Tên")]
        public string Sulg { get; set; }
        [Display(Name = "Hiển thị")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        [Display(Name = "Xóa Id")]
        public Nullable<bool> DeleteId { get; set; }
       
    }
}