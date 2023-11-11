using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebsiteBanHang.Common;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        ShopModelEntities objShopModelEntities = new ShopModelEntities();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
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
                //Lấy ds sản phẩm theo từ khóa tìm kiếm
                lstCategory = objShopModelEntities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstCategory = objShopModelEntities.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //Số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Create(Category objCategory)
        {
            this.LoadData();
            try
            {
                if (objCategory.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                    objCategory.Avartar = fileName;
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/categoty/"), fileName));
                    objShopModelEntities.Categories.Add(objCategory);
                    objShopModelEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        void LoadData()
        {
            Common objCommon = new Common();
            //Lấy dữ liệu danh mục dưới database
            var lstCat = objShopModelEntities.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            //Convert sang select list dạng value, text
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //Lấy dữ liệu danh mục dưới database
            var lstBrand = objShopModelEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //Convert sang select list dạng value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //Convert sang select list dạng value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objShopModelEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objShopModelEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category objCat)
        {
            var objCategory = objShopModelEntities.Categories.Where(n => n.Id == objCat.Id).FirstOrDefault();
            objShopModelEntities.Categories.Remove(objCategory);
            objShopModelEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objShopModelEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                objCategory.Avartar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/categoty/"), fileName));
            }
            objShopModelEntities.Entry(objCategory).State = EntityState.Modified;
            objShopModelEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}