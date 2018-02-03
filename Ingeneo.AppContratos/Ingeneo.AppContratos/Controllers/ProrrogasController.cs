using Ingeneo.AppContratos.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ingeneo.AppContratos.Controllers
{
    public class ProrrogasController : Controller
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

        // GET: Prorrogas/Details/5        
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La prórroga del contrato que busca no existe en la base de datos, debes intentarlo de nuevo.'," +
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

        // GET: Prorrogas/Create
        [CustomAuth(Roles = "Monitor, Admin, Analista")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {

                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La prórroga que deseas crear no tiene una clave del contrato al cual debe hacer referencia, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");

            }

            var ValDateEnd = db.Contrato.Where(x => x.id == id).FirstOrDefault();

            if (ValDateEnd.FechaFinContrato != null)
            {             

                var ValProrroga = db.Prorroga.Where(e => e.Contratoid == id && e.FechaFinProrroga == null)
                                  .FirstOrDefault();
                
                if (ValProrroga == null)
                {
                    Contrato contrato = db.Contrato.Find(id);

                    if (contrato == null)
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
                    ViewBag.Contratoid = contrato.id;
                    return View();
                }
                else {
                    string hh = Url.Content("~/Content/sweetalert2.min.css");
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<Script>swal({title: 'Acción incorrecta..'," +
                                   "text: 'No puedes agregar otra prórroga a este contrato ya que tiene una prórroga a termino indefinido, cuando establezca la fecha fin de la prórroga puede agregar otra.'," +
                                   "type: 'error'," +
                                   "showCancelButton: false," +
                                   "confirmButtonColor: '#3085d6'," +
                                   "cancelButtonColor: '#d33'," +
                                   "confirmButtonText: 'Aceptar!'}).then(function() " +
                                   "{swal(''," +
                                   "''," +
                                   "'success', window.location.href='/Contratos/Index')});</script>");


                }
            }
            else
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Acción incorrecta..'," +
                               "text: 'No puedes agregar una prórroga a este contrato ya que es a termino indefinido, cuando establezca la fecha fin del contrato puede agregar una prórroga.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
        }

        // POST: Prorrogas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FechaInicioProrroga,FechaFinProrroga,Contratoid")] Prorroga prorroga)
        {
            if (ModelState.IsValid)
            {
                db.Prorroga.Add(prorroga);
                db.SaveChanges();
                return Redirect("/Contratos/Details/" + prorroga.Contratoid);
            }

            ViewBag.Contratoid =  prorroga.Contratoid;
            return View(prorroga);
        }

        // GET: Prorrogas/Edit/5
        [CustomAuth(Roles = "Monitor, Admin")]        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La prórroga que deseas editar no existe en la base de datos, debes intentarlo de nuevo.'," +
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
            ViewBag.Contratoid =  prorroga.Contratoid;
            return View(prorroga);
        }

        // POST: Prorrogas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FechaInicioProrroga,FechaFinProrroga,Contratoid")] Prorroga prorroga)
        {
            if (ModelState.IsValid)
            {
                var ValMoreExtension = db.Prorroga.AsNoTracking().Where(x => x.Contratoid == prorroga.Contratoid).ToList().Count();

                
                var ValLastExtensionSaved = db.Prorroga.AsNoTracking().Where(e => e.Contratoid == prorroga.Contratoid).Max(x => x.id);
                
                

                if (ValLastExtensionSaved == prorroga.id)
                {
                    db.Entry(prorroga).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect("/Contratos/Details/" + prorroga.Contratoid);
                }
                else
                {
                    if (prorroga.FechaFinProrroga == null)
                    {
                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<Script>swal({title: 'Acción Incorrecta..'," +
                                       "text: 'Esta prórroga no puedes editarla con fecha fin nula porque hay otras prórrogas superiores a ella.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/Contratos/Details/" + prorroga.Contratoid + "')});</script>");

                    }
                    else
                    {
                        db.Entry(prorroga).State = EntityState.Modified;
                        db.SaveChanges();
                        return Redirect("/Contratos/Details/" + prorroga.Contratoid);

                    }
                }

            }
            ViewBag.Contratoid = prorroga.Contratoid;
            return View(prorroga);
        }


        // GET: Prorrogas/Delete/5        
        [CustomAuth(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'La prórroga que desea eliminar no existe en la base de datos, debes intentarlo de nuevo.'," +
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

        // POST: Prorrogas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prorroga prorroga = db.Prorroga.Find(id);
            ViewBag.Contratoid = prorroga.Contratoid;
            db.Prorroga.Remove(prorroga);
            try
            {
                db.SaveChanges();
            }
            catch (Exception) {

                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'No puedes realizar esta acción..'," +
                               "text: 'Hay una póliza asociada a esta prórroga, elimine esas dependencias para proceder a eliminar este registro.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal('Hecho!'," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            return Redirect("/Contratos/Details/"+ ViewBag.Contratoid);
            
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
