using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        ShopModelEntities objShopModelEntities = new ShopModelEntities();
        public ActionResult Index()
        {
            var lstCategory = objShopModelEntities.Categories.ToList();
            return View(lstCategory);
           
        }
        public ActionResult ProductCategory(int Id)
        {
            ShopModelEntities objShopModelEntities = new ShopModelEntities();
            var listProduct = objShopModelEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}