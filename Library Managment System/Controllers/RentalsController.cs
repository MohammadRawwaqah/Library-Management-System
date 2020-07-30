using Library_Managment_System.Dtos;
using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Managment_System.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals/Rent
        [Authorize]
        public ActionResult Rent()
        {
            using (Db db = new Db())
            {
                NewRentalDto newrentaldto = new NewRentalDto();


                ViewBag.categories = db.Categories.ToList();
                ViewBag.users = db.Users.ToList();


                return View(newrentaldto);

            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult Rent(NewRentalDto newrentaldto)
        {

            return View();
        }




    }
}
