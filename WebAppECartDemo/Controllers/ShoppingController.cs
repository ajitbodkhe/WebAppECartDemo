using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppECartDemo.Models;
using WebAppECartDemo.ViewModel;

namespace WebAppECartDemo.Controllers
{
    public class ShoppingController : Controller
    {
        WebAppECartDemo_dbEntities eCartDBEntities;

        public ShoppingController()
        {
            eCartDBEntities = new WebAppECartDemo_dbEntities();
        }

        // GET: Shopping
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> listShoppingViewModel = (from objItem in eCartDBEntities.Items
                                                                    join objCate in eCartDBEntities.Categories
                                                                    on objItem.CategoryId equals objCate.CategoryId
                                                                    select new ShoppingViewModel
                                                                    {
                                                                        ImagePath = objItem.ImagePath,
                                                                        ItemName = objItem.ItemName,
                                                                        Description = objItem.Description,
                                                                        ItemPrice = objItem.ItemPrice,
                                                                        ItemCode = objItem.ItemCode,
                                                                        ItemId = objItem.ItemId,
                                                                        Category = objCate.CategoryName
                                                                    }
                                                                    ).ToList();
            return View(listShoppingViewModel);
        }
    }
}