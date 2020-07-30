using Library_Managment_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Library_Managment_System.Controllers.Api
{
    public class UsersController : ApiController
    {
        [HttpGet]
        [Authorize]
        //NOTE: I don't use this api anymore, Because system should know userName logged in not 
        //he select his name between all users we have in our system.
        public IHttpActionResult GetUsers(string query = null)
        {
            using (Db db = new Db())
            {
                var usersquery = db.Users.AsQueryable();
                if (!string.IsNullOrWhiteSpace(query))
                    usersquery = usersquery.Where(m => m.UserName.Contains(query));
                var users = usersquery.ToList();
                return Ok(users);
            }
        }




    }
}
