using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Managment_System.Controllers
{
    public class CategoryController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Category
        public ActionResult Index()
        {
            using (Db db = new Db())
            {
                var categories = db.Categories.ToList();
                return View(categories);

            }
        }

    }
}