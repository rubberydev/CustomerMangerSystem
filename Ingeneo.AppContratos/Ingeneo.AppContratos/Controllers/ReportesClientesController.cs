using System.Net;
using System.Web.Mvc;
using Ingeneo.AppContratos.Models;

namespace Ingeneo.AppContratos.Controllers
{
    public class ReportesClientesController : Controller
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

        // GET: ReportesClientes/Details/5
        [CustomAuth(Roles = "Monitor, Admin, Analista")]        
        public ActionResult Reportes(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El reporte del cliente que desea generar no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
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
                               "'success', window.location.href='/Clientes/Index')});</script>");
            }
            return View(cliente);
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
