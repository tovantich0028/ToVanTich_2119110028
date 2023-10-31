﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ShopModelEntities objShopModelEntities = new ShopModelEntities();
        public ActionResult Detail(int Id)
        {
            var objProduct = objShopModelEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}