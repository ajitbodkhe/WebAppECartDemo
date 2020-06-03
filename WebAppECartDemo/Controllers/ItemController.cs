using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppECartDemo.Models;
using WebAppECartDemo.ViewModel;

namespace WebAppECartDemo.Controllers
{
    public class ItemController : Controller
    {
        WebAppECartDemo_dbEntities eCartDBEntities;

        public ItemController()
        {
            eCartDBEntities = new WebAppECartDemo_dbEntities();
        }

        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel itemViewModel = new ItemViewModel();
            itemViewModel.categoryItemList = from itemObj in eCartDBEntities.Categories
                                             select new SelectListItem
                                             {
                                                 Text = itemObj.CategoryName,
                                                 Value = itemObj.CategoryId.ToString(),
                                                 Selected = true

                                             };
            return View(itemViewModel);
        }

        [HttpPost]
        public ActionResult AddItem(ItemViewModel itemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(itemViewModel.ImagePath.FileName);
            itemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/"+NewImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + NewImage;
            objItem.CategoryId = itemViewModel.CategoryId;
            objItem.Description = itemViewModel.Description;
            objItem.ItemCode = itemViewModel.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            //This is test.
            objItem.ItemName = itemViewModel.ItemName;
            objItem.ItemPrice = itemViewModel.ItemPrice;
            eCartDBEntities.Items.Add(objItem);
            eCartDBEntities.SaveChanges();

            return Json(new { Success = true, Message = "Item Added Successfully", JsonRequestBehavior.AllowGet });
        }
    }
}