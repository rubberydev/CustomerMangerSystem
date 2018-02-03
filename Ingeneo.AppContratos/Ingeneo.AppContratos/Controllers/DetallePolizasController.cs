using Ingeneo.AppContratos.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ingeneo.AppContratos.Controllers
{
    public class DetallePolizasController : Controller
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
        // GET: DetallePolizas
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Index()
        {
            var detallePoliza = db.DetallePoliza.Include(d => d.Poliza);
            return View(detallePoliza.ToList());
        }

        // GET: DetallePolizas/Details/5
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El detalle de la cobertura que desea ver no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            DetallePoliza detallePoliza = db.DetallePoliza.Find(id);
            if (detallePoliza == null)
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
            return View(detallePoliza);
        }

        // GET: DetallePolizas/Create
        [CustomAuth(Roles = "Monitor, Admin, Analista")]
        public ActionResult Create(int? id)
        {
            if (id == null) {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La cobertura de la póliza que deseas crear no tiene una clave de la póliza asociada, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }

            Poliza poliza = db.Poliza.Find(id);

            if (poliza == null) {

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
            ViewBag.PolizaId = poliza.id;
            return View();
        }

        // POST: DetallePolizas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FechaInicioProteccion,FechaFinProteccion,PolizaId,DescripcionCobertura")] DetallePoliza detallePoliza)
        {
            if (ModelState.IsValid)
            {
               
                var ValDescrip = db.DetallePoliza
                    .Where(x => x.PolizaId == detallePoliza.PolizaId && x.DescripcionCobertura == detallePoliza.DescripcionCobertura)
                    .FirstOrDefault();

                if (ValDescrip == null)
                {
                    db.DetallePoliza.Add(detallePoliza);
                    db.SaveChanges();                  


                    Poliza poliza = db.Poliza.Find(detallePoliza.PolizaId);
                    if (poliza == null)
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
                    if (poliza.idContrato != null)
                    {
                        return Redirect("/Polizas/Details/" + poliza.idContrato);
                    }
                    else
                    {
                        return Redirect("/PolizasProrroga/Details/" + poliza.idProrroga);
                    }

                }
                else
                {
                        var pol = db.Poliza.Find(detallePoliza.PolizaId);
                        var Det1 = pol.idContrato;

                        if (Det1 != null)
                        {
                            string hh = Url.Content("~/Content/sweetalert2.min.css");
                            return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                           "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                           "<script>swal({title: 'ERROR..'," +
                                           "text: 'La cobertura de esta póliza que deseas registrar ya existe en la base de datos con esta misma descripción.'," +
                                           "type: 'error'," +
                                           "showCancelButton: false," +
                                           "confirmButtonColor: '#3085d6'," +
                                           "cancelButtonColor: '#d33'," +
                                           "confirmButtonText: 'Aceptar!'}).then(function() " +
                                           "{swal(''," +
                                           "''," +
                                           "'success', window.location.href='/Polizas/Details/" + Det1 + "')});</script>");
                        }
                        else
                        {

                            var Det2 = pol.idProrroga;
                            string hh = Url.Content("~/Content/sweetalert2.min.css");
                            return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                           "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                           "<script>swal({title: 'ERROR..'," +
                                           "text: 'La cobertura de esta póliza que deseas registrar ya existe en la base de datos con esta misma descripción.'," +
                                           "type: 'error'," +
                                           "showCancelButton: false," +
                                           "confirmButtonColor: '#3085d6'," +
                                           "cancelButtonColor: '#d33'," +
                                           "confirmButtonText: 'Aceptar!'}).then(function() " +
                                           "{swal(''," +
                                           "''," +
                                           "'success', window.location.href='/PolizasProrroga/Details/" + Det2 + "')});</script>");

                        }
                }
            }
            ViewBag.PolizaId = detallePoliza.PolizaId;
            return View(detallePoliza);
        }

        // GET: DetallePolizas/Edit/5
        [CustomAuth(Roles = "Monitor, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La cobertura de la póliza que deseas editar no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            DetallePoliza detallePoliza = db.DetallePoliza.Find(id);
            if (detallePoliza == null)
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
            ViewBag.PolizaId = detallePoliza.PolizaId;
            return View(detallePoliza);
        }

        // POST: DetallePolizas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FechaInicioProteccion,FechaFinProteccion,PolizaId,DescripcionCobertura")] DetallePoliza detallePoliza)
        {
            if (ModelState.IsValid)
            {
                var ValDescrip = db.DetallePoliza
                    .AsNoTracking().Where(x => x.PolizaId == detallePoliza.PolizaId && x.DescripcionCobertura == detallePoliza.DescripcionCobertura)
                    .FirstOrDefault();

                if (ValDescrip == null || ValDescrip.DescripcionCobertura == detallePoliza.DescripcionCobertura)
                {

                    db.Entry(detallePoliza).State = EntityState.Modified;
                    db.SaveChanges();

                    Poliza poliza = db.Poliza.Find(detallePoliza.PolizaId);
                    if (poliza == null)
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
                    if (poliza.idContrato != null)
                    {
                        return Redirect("/Polizas/Details/" + poliza.idContrato);
                    }
                    else
                    {
                        return Redirect("/PolizasProrroga/Details/" + poliza.idProrroga);
                    }
                }
                else {
                    var pol = db.Poliza.Find(detallePoliza.PolizaId);
                    var Det1 = pol.idContrato;

                    if (Det1 != null)
                    {

                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'La cobertura de esta póliza que deseas editar ya existe en la base de datos con esta misma descripción.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/Polizas/Details/" + Det1 + "')});</script>");
                    }
                    else
                    {

                        var Det2 = pol.idProrroga;
                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'La cobertura de esta póliza que deseas editar ya existe en la base de datos con esta misma descripción.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/PolizasProrroga/Details/" + Det2 + "')});</script>");

                    }
                }
            }
            ViewBag.PolizaId = detallePoliza.PolizaId;
            return View(detallePoliza);
        }

        // GET: DetallePolizas/Delete/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La cobertura de la póliza que deseas eliminar no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            DetallePoliza detallePoliza = db.DetallePoliza.Find(id);
            if (detallePoliza == null)
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
            return View(detallePoliza);
        }

        // POST: DetallePolizas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePoliza detallePoliza = db.DetallePoliza.Find(id);
            db.DetallePoliza.Remove(detallePoliza);
            db.SaveChanges();

            Poliza poliza = db.Poliza.Find(detallePoliza.PolizaId);
            if (poliza == null)
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
                               "{swal('Hecho!'," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            if (poliza.idContrato != null)
            {
                return Redirect("/Polizas/Details/" + poliza.idContrato);
            }
            else
            {
                return Redirect("/PolizasProrroga/Details/" + poliza.idProrroga);
            }
            
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
