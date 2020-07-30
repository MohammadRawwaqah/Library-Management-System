using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Managment_System.Controllers.Api
{
    public class CategoriesController : ApiController
    {
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public string AddCategory([FromBody]string cname)
        {
            using(Db db = new Db())
            {
                if(db.Categories.Any(c=>c.Name == cname))
                {
                    return "TitleTaken";
                }
                else
                {
                    Category category = new Category();
                    category.Name = cname;
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return category.Id.ToString();
                }

            }

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            using (Db db = new Db())
            {
                var CategoryToRemove = db.Categories.SingleOrDefault(c => c.Id == id);
                db.Categories.Remove(CategoryToRemove);
                db.SaveChanges();
            }
            return Ok();
        }

    }
}
