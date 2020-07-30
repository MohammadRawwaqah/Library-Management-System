using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Http;
using System.Web.Routing;
using System.Data.Entity;
using Library_Managment_System.App_Start;
using Library_Managment_System.Models;
using System.Security.Principal;

namespace Library_Managment_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            if (User == null) { return; }

            string username = Context.User.Identity.Name;

            string[] roles = null;

            using (Db db = new Db())
            {
                User dto = db.Users.FirstOrDefault(x => x.UserName == username);

                //we get all roles that belong to logged user and we used Navigation property x.Role
                //to reach Names of roles we want ......look we used select---->select meaning return 
                //because roles it is an array -----> string[] roles
                roles = db.UserRoles.Where(x => x.UserId == dto.Id).Select(x => x.Role.Name).ToArray();
            }

            IIdentity userIdentity = new GenericIdentity(username);
            IPrincipal newUserObj = new GenericPrincipal(userIdentity, roles);

            Context.User = newUserObj;
        }
    }
}
