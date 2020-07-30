using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Managment_System.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books/Index
        public ActionResult Index()
        {
            using (Db db = new Db())
            {
                //You Must include Category to load it in memory, so you can play with in view
                //so if you don't include it, you can't type this book.category.Name
                //why because you don't load category in memory.
                var books = db.Books.Include("Category").ToList();

                return View(books);

            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddBook()
        {
            Book book = new Book();
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            using (Db db = new Db())
            {
                db.Books.Add(book);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        }


    }
}