using Library_Managment_System.Dtos;
using Library_Managment_System.Models;
using Library_Managment_System.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Library_Managment_System.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //LOGIN START************************************************************************

        [HttpGet]
        public ActionResult Login()
        {
            string username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
                return RedirectToAction("CreateAccount");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isValid = false;

            using (Db db = new Db())
            {
                if (db.Users.Any(x => x.UserName.Equals(model.UserName) && x.Password.Equals(model.Password)))
                {
                    isValid = true;
                }
            }

            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return Redirect(FormsAuthentication.GetRedirectUrl(model.UserName, model.RememberMe));
            }

            return View();
        }
        //LOGIN END************************************************************************

        public ActionResult UserNavPartial()
        {
            string username = User.Identity.Name;

            UserNavPartialDTO model;

            using (Db db = new Db())
            {
                User dto = db.Users.FirstOrDefault(x => x.UserName == username);

                model = new UserNavPartialDTO()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

            }

            return PartialView(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //return RedirectToAction("Login"); You can write this also but the one below is nicer;
            return Redirect("~/account/login");
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            using (Db db = new Db())
            {
                var membershipTypes = db.MembershipTypes.ToList();
                UserRegisterFormVM ViewModel = new UserRegisterFormVM()
                {
                    MembershipTypes = membershipTypes,
                    user = new UserDto()
                };

                return View(ViewModel);

            }
        }

        //I use this action Save[HttpGet] just to bring userName for user that
        //loggedIn, to send this user to Save action[HttpPost] to edit it
        [HttpGet]
        [Authorize]
        public ActionResult Save()
        {
            using (Db db = new Db())
            {
                //user loggedIn
                string username = User.Identity.Name;
                var UserInDb = db.Users.SingleOrDefault(u => u.UserName == username);
                UserDto Userdto = new UserDto()
                {
                    Id = UserInDb.Id,
                    FirstName = UserInDb.FirstName,
                    EmailAddress = UserInDb.EmailAddress,
                    LastName = UserInDb.LastName,
                    MembershipTypeId = UserInDb.MembershipTypeId,
                    Password = UserInDb.Password,
                    UserName = UserInDb.UserName
                };

                UserRegisterFormVM vm = new UserRegisterFormVM()
                {
                    user = Userdto,
                    MembershipTypes = db.MembershipTypes.ToList()

                };
                return View("CreateAccount", vm);
            }
        }

        //this is [HttpPost] for CreateAccount()
        //and it is for edit accounts.
        [HttpPost]
        public ActionResult Save(UserRegisterFormVM viewmodel)
        {
            using (Db db = new Db())
            {
                viewmodel.MembershipTypes = db.MembershipTypes.ToList();

                if (!ModelState.IsValid)
                {
                    return View("CreateAccount", viewmodel);
                }

                if (!viewmodel.user.Password.Equals(viewmodel.user.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords don't match");
                    return View("CreateAccount", viewmodel);
                }

                if (db.Users.Any(x => x.UserName.Equals(viewmodel.user.UserName)))
                {
                    ModelState.AddModelError("", "Username" + viewmodel.user.UserName + " is taken");
                    viewmodel.user.UserName = "";
                    return View("CreateAccount", viewmodel);
                }



                if (viewmodel.user.Id == 0)
                {

                    User userToCreate = new User();

                    userToCreate.FirstName = viewmodel.user.FirstName;
                    userToCreate.EmailAddress = viewmodel.user.EmailAddress;
                    userToCreate.LastName = viewmodel.user.LastName;
                    userToCreate.MembershipTypeId = viewmodel.user.MembershipTypeId;
                    userToCreate.Password = viewmodel.user.Password;
                    userToCreate.UserName = viewmodel.user.UserName;

                    //userroles here before

                    db.Users.Add(userToCreate);

                    db.SaveChanges();

                    int id = userToCreate.Id;

                    UserRole userRolesDTO = new UserRole()
                    {
                        UserId = id,
                        RoleId = 2

                    };

                    db.UserRoles.Add(userRolesDTO);
                    db.SaveChanges();

                }
                else
                {
                    var userInDb = db.Users.Single(u => u.Id == viewmodel.user.Id);

                    userInDb.FirstName = viewmodel.user.FirstName;
                    userInDb.EmailAddress = viewmodel.user.EmailAddress;
                    userInDb.LastName = viewmodel.user.LastName;
                    userInDb.MembershipTypeId = viewmodel.user.MembershipTypeId;
                    userInDb.Password = viewmodel.user.Password;
                    userInDb.UserName = viewmodel.user.UserName;

                    db.SaveChanges();
                }

                return Redirect("~/account/login");
            }
        }
    }
}