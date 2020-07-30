using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Managment_System.Controllers.Api
{
    public class BooksController : ApiController
    {
        [Authorize]
        public IHttpActionResult GetBooks(string query = null)
        {
            using (Db db = new Db())
            {
                var booksquery = db.Books.Where(b => b.NumberAvailable > 0);
                if (!string.IsNullOrWhiteSpace(query))
                    booksquery = booksquery.Where(m => m.Name.Contains(query));
                var books = booksquery.ToList();
                return Ok(books);
            }
        }

    }
}
