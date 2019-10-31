using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDragDropJqueryAspMvc.Models;

namespace TestDragDropJqueryAspMvc.Controllers
{
    public class TaskBoardController : Controller
    {
        Test3Entities db = new Test3Entities();
        public ActionResult Index()
        {
            var item = db.Post.ToList();
            var item2 = item.OrderBy(x => x.RowNo);
            return View(item2.ToList());
        }
        public ActionResult UpdateItem(string itemIds) 
        {
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach (var itemId in itemIds) 
            {
                try 
                {
                    Post item = db.Post.Where(x => x.Id == itemId).FirstOrDefault();
                    item.RowNo = count;
                    db.Post.AddOrUpdate(item);
                    db.SaveChanges();
                }
                catch (Exception) 
                {
                    continue;
                }
                count++;
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}