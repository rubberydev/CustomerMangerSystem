using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Ingeneo.AppContratos.Models;
using System;
using System.Linq;

namespace Ingeneo.AppContratos.Controllers
{
    public class PolizasProrrogaController : Controller
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();

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


        // GET: PolizasProrroga/Details/5
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El detalle de la póliza que desea ver no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            Prorroga prorroga = db.Prorroga.Find(id);
            if (prorroga == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'El directorio especificado no existe en el servidor web..'," +
                               "text: 'Se ha quitado el recurso que está buscando, se le ha cambiado el nombre o no está disponible en estos momentos.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            return View(prorroga);
        }

        // GET: PolizasProrroga/Create
        [CustomAuth(Roles = "Monitor, Admin, Analista")]
        public ActionResult Create(int? id)
        {
            if (id == null) {

                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La póliza que deseas crear no tiene una clave de la prórroga a la cual debe hacer referencia, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            Prorroga prorroga = db.Prorroga.Find(id);
            if (prorroga == null) {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'El directorio especificado no existe en el servidor web..'," +
                               "text: 'Se ha quitado el recurso que está buscando, se le ha cambiado el nombre o no está disponible en estos momentos.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }  
           
            ViewBag.idProrroga = prorroga.id;
            return View();

           
        }

        // POST: PolizasProrroga/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CodigoPoliza,NombreAseguradora,FechaInicioPoliza,FechaFinpoliza,idContrato,idProrroga")] Poliza poliza)
        {
            
            Poliza ValPolpro = db.Poliza.Where(x => x.idProrroga == poliza.idProrroga).FirstOrDefault();           

            
            if (ValPolpro == null)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Poliza.Add(poliza);
                        db.SaveChanges();
                        return Redirect("/PolizasProrroga/Details/" + poliza.idProrroga);
                    }
                    catch (Exception)
                    {

                        var idP = poliza.idProrroga;
                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'Esta prórroga ya tiene una póliza registrada.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/PolizasProrroga/Details/"+idP+"')});</script>");

                    }
                }
            }
            else
            {
                var idP = poliza.idProrroga;
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<script>swal({title: 'ERROR..'," +
                               "text: 'Esta prórroga ya tiene una póliza registrada.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/PolizasProrroga/Details/" + idP + "')});</script>");

            }

            ViewBag.idProrroga =  poliza.idProrroga;
            return View(poliza);
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
