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
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        ShopModelEntities objShopModelEntities = new ShopModelEntities();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
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
                lstBrand = objShopModelEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstBrand = objShopModelEntities.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //Số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            this.LoadData();
            try
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                    objBrand.Avartar = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    objShopModelEntities.Brands.Add(objBrand);
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
            var objBrand = objShopModelEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objShopModelEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        public ActionResult Delete(Brand objBr)
        {
            var objBrand = objShopModelEntities.Brands.Where(n => n.Id == objBr.Id).FirstOrDefault();
            objShopModelEntities.Brands.Remove(objBrand);
            objShopModelEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objShopModelEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        public ActionResult Edit(int id, Brand objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                objBrand.Avartar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objShopModelEntities.Entry(objBrand).State = EntityState.Modified;
            objShopModelEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}