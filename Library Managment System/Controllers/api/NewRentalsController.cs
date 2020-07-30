using Library_Managment_System.Dtos;
using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Managment_System.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        [Authorize]
        [HttpPost]
        public IHttpActionResult RentBook(IEnumerable<int?> BooksIds)
        {
            using (Db db = new Db())
            {

                //why i remark this, because you shouldn't ask user for his name, it is logged in already!!!
                //so user shouldn't be sended.
                //var user = db.Users.SingleOrDefault(u => u.Id == NewRental.UserId);

                var Username = User.Identity.Name;
                var user = db.Users.SingleOrDefault(u => u.UserName == Username);
                if (BooksIds != null)
                {

                    var books = db.Books.Where(b => BooksIds.Contains(b.Id)).ToList();

                    foreach (var book in books)
                    {
                        var rental = new Rental()
                        {
                            User = user,
                            Book = book

                        };
                        db.Rentals.Add(rental);
                        --book.NumberAvailable;

                    }

                    db.SaveChanges();
                }
                return Ok();

            }

        }
    }
}
