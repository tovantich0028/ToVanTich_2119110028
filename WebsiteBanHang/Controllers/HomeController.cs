using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        ShopModelEntities objModel = new ShopModelEntities();
        public ActionResult Index()
        {
            var lstCategory = objModel.Categories.ToList();
            var lstProduct = objModel.Products.ToList();
           
            return View();
        }

       
        
    }
}