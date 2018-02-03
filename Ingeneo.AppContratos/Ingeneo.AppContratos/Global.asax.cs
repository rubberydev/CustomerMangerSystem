using Ingeneo.AppContratos.Models;
using Ingeneo.AppContratos.ModelViews;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Quartz;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Converters;

namespace Ingeneo.AppContratos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var dateTimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "dd MMMM yyyy"
            };
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                               .Converters.Add(dateTimeConverter);

            //Configuracion para consumir servicios web API
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //Permitir migrationes automaticas cuando yo haga algun cambio en el modelo
            Database.SetInitializer(new
                MigrateDatabaseToLatestVersion
                <Models.GestorContratosDbContext,
                Migrations.Configuration>());


            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);
            AddPermisionsToSuperUser(db);
            db.Dispose();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JobSchedule.Start();
        }

        private void AddPermisionsToSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.FindByName("alejandro.salazar@ingeneo.com.co");

            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                userManager.AddToRole(user.Id, "Admin");
            }
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("alejandro.salazar@ingeneo.com.co");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "alejandro.salazar@ingeneo.com.co",
                    Email = "alejandro.salazar@ingeneo.com.co",
                };
                userManager.Create(user, "Admin123.");
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));            

            if (!roleManager.RoleExists("Analista"))
            {
                roleManager.Create(new IdentityRole("Analista"));
            }

            if (!roleManager.RoleExists("Monitor"))
            {
                roleManager.Create(new IdentityRole("Monitor"));
            }

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
        }
    }
}
    