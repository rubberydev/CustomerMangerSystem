using Ingeneo.AppContratos.Models;
using Ingeneo.AppContratos.ModelViews;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ingeneo.AppContratos.Controllers
{
    public class UsersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public class CustomAuth : AuthorizeAttribute
        {
            public string RedirectUrl = "/Account/Login";
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                base.OnAuthorization(filterContext);
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult(RedirectUrl);
                    return;
                }
                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    //Descomentar esta linea para usar JS alert enves de redireccionar a otro pagina
                    //filterContext.Result = new ContentResult { ContentType = "text/html", Content = "<script>alert('MAMBRU SE FUE A LA GUERRA!!! :@');window.location.href ='/Home/Index';</script> " };

                    //filterContext.Controller.TempData.Add("RedirectReason", "Acceso Denegado!!! Prenda un porro!");
                    filterContext.Result = new RedirectResult("/Home/Denied");
                    return;
                }
            }
        }

        // GET: Users        
        [CustomAuth(Roles = "Admin")]
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.UserName,
                    UserID = user.Id
                };
                usersView.Add(userView);
            }
            return View(usersView);
        }

        [CustomAuth(Roles = "Admin")]
        public ActionResult Roles(string userID)
        {

            if (string.IsNullOrWhiteSpace(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userID);
            if (user == null)
            {
                return HttpNotFound();
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                var role = roles.Find(x => x.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View(userView);
        }

        [CustomAuth(Roles = "Admin")]
        public ActionResult AddRole(string userID)
        {

            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Seleccione un Rol....]" });
            list = list.OrderBy(x => x.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name");

            return View(userView);
        }

        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            var roleID = Request["RoleID"];

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userID);

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.Error = "Debe de seleccionar un Rol";
                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Seleccione un Rol....]" });
                list = list.OrderBy(x => x.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name");
                return View(userView);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(x => x.Id == roleID);

            if (!userManager.IsInRole(userID, role.Name))
            {
                userManager.AddToRole(userID, role.Name);
            }

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(x => x.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                Roles = rolesView,
                UserID = user.Id
            };

            return View("Roles", userView);

        }

        [CustomAuth(Roles = "Admin")]
        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.Users.ToList().Find(x => x.Id == userID);
            var role = roleManager.Roles.ToList().Find(x => x.Id == roleID);


            //Delete user from role
            if (userManager.IsInRole(userID, role.Name))
            {
                userManager.RemoveFromRole(userID, role.Name);
            }

            //Prepare de view to return
            var users = userManager.Users.ToList();            
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(x => x.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);
        }


        [CustomAuth(Roles = "Admin")]
        public ActionResult DeleteUser(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.Users.ToList().Find(x => x.Id == userID);
            var role = roleManager.Roles.ToList().Find(x => x.Id == roleID);


            //Delete user from role
            if (string.IsNullOrEmpty(roleID))
            {
                //userManager.RemoveFromRole(userID, role.Name);
                db.Users.Remove(db.Users.Where(x => x.Id == user.Id).Single());
                db.SaveChanges();
            }
            else
            {
                if (userManager.IsInRole(userID, role.Name))
                {
                    userManager.RemoveFromRole(userID, role.Name);
                    db.Users.Remove(db.Users.Where(x => x.Id == user.Id).Single());
                    db.SaveChanges();
                }
            }
            //Prepare de view to return
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();
            var usersView = new List<UserView>();

            foreach (var item in users)
            {
                var userView = new UserView
                {
                    Email = item.Email,
                    Name = item.UserName,
                    UserID = item.Id
                };
                usersView.Add(userView);
            }
            return View("Index", usersView);         

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    
}