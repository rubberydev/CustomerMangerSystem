using Ingeneo.AppContratos.Models;
using Ingeneo.AppContratos.ModelViews;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Ingeneo.AppContratos.Controllers
{
    public class NotificationController : Controller
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();
        public DateTime FirstN = DateTime.Today.AddDays(10);        
        public DateTime SecN = DateTime.Today.AddDays(5);

        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }
    }
}