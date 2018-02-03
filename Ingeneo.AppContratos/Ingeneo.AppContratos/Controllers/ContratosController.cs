using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ingeneo.AppContratos.Models;
using Ingeneo.AppContratos.ModelViews;
using System.Collections.Generic;

namespace Ingeneo.AppContratos.Controllers
{
    public class ContratosController : Controller
    {        
        private GestorContratosDbContext db = new GestorContratosDbContext();
        public DateTime FirstN = DateTime.Today.AddDays(10);
        public DateTime SecondN = DateTime.Today.AddDays(5);

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
                    
                    filterContext.Result = new RedirectResult("/Home/Denied");
                    return;
                }
            }
        }
        // GET: Contratos
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Index(string pNombre)
        {
            string contr = pNombre;
            if (contr == "Inactivos")
            {
                var contrato = db.Contrato.Where(c => c.EstadoContrato == true);
                ViewData["valor"] = "inactivos";

                
                var contratosN = db.Contrato.ToList();
                var contratosNView = new List<Notification>();
                foreach (var cont in contratosN)
                {
                    if (cont.FechaFinContrato.HasValue)
                    {
                        var DateN = cont.FechaFinContrato.Value;
                        if (FirstN.Year == DateN.Year)
                        {
                            if (FirstN.Month == DateN.Month)
                            {
                                if (FirstN.Day == DateN.Day)
                                {                                   
                                    ViewBag.azul = true;
                                }
                            }
                        }
                    }
                }               

                foreach (var cont in contratosN)
                {
                    if (cont.FechaFinContrato.HasValue)
                    {
                        var DateN = cont.FechaFinContrato.Value;
                        if (SecondN.Year == DateN.Year)
                        {
                            if (SecondN.Month == DateN.Month)
                            {
                                if (SecondN.Day == DateN.Day)
                                {
                                    ViewBag.x = true;
                                }
                            }
                        }
                    }
                }
                return View(contrato.ToList());

            }
            else {
                var contrato = db.Contrato.Where(c => c.EstadoContrato == false);               
                ViewData["valor"] = "activos";

                if (true)
                {

                }

                var contratosN = db.Contrato.ToList();
                var contratosNView = new List<Notification>();
                foreach (var cont in contratosN)
                {
                    if (cont.FechaFinContrato.HasValue)
                    {
                        var DateN = cont.FechaFinContrato.Value;
                        if (FirstN.Year == DateN.Year || SecondN.Year == DateN.Year)
                        {
                            if (FirstN.Month == DateN.Month || SecondN.Month == DateN.Month)
                            {
                                if (FirstN.Day == DateN.Day || SecondN.Day == DateN.Day)
                                {
                                    ViewBag.blue = true;
                                }
                            }
                        }
                    }
                }

                var prorrogas = db.Prorroga.ToList();
                foreach (var pro in prorrogas)
                {
                    if (pro.FechaFinProrroga.HasValue)
                    {
                        var DateN = pro.FechaFinProrroga.Value;
                        if (FirstN.Year == DateN.Year || SecondN.Year == DateN.Year)
                        {
                            if (FirstN.Month == DateN.Month || SecondN.Month == DateN.Month)
                            {
                                if (FirstN.Day == DateN.Day || SecondN.Day == DateN.Day)
                                {
                                    ViewBag.green = true;
                                }
                            }
                        }
                    }
                }

                var polizas = db.Poliza.ToList();
                foreach (var pol in polizas)
                {
                    if (pol.FechaFinpoliza != null)
                    {
                        var DateN = pol.FechaFinpoliza;
                        if (FirstN.Year == DateN.Year || SecondN.Year == DateN.Year)
                        {
                            if (FirstN.Month == DateN.Month || SecondN.Month == DateN.Month)
                            {
                                if (FirstN.Day == DateN.Day || SecondN.Day == DateN.Day)
                                {
                                    ViewBag.yellow = true;
                                }
                            }
                        }
                    }
                }                
                return View(contrato.OrderBy(x => x.Cliente.NombreCliente).ToList());                
            }         
        }


        // GET: Contratos/Details/5
        [CustomAuth(Roles = "Analista, Monitor, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El detalle del contrato que busca no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
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
            return View(contrato);
        }

        // GET: Contratos/Create
        [CustomAuth(Roles = "Monitor, Admin, Analista")]
        public ActionResult Create()
        {
            //ViewBag.Clienteid = new SelectList(db.Cliente, "id", "NombreCliente");
            ViewBag.Clienteid = new SelectList(db.Cliente.OrderBy(x => x.NombreCliente), "id", "NombreCliente");
            return View();
        }

        // POST: Contratos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CodigoContrato,FechaInicioContrato,FechaFinContrato,Clienteid,ValorContrato,EstadoContrato")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RepeatName = db.Contrato.Where(x => x.Clienteid == contrato.Clienteid && x.CodigoContrato == contrato.CodigoContrato).FirstOrDefault();

                    if (RepeatName == null)
                    {

                        db.Contrato.Add(contrato);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else {
                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'El contrato que deseas registrar ya existe para este cliente.'," +
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
                catch (Exception) {
                    string hh = Url.Content("~/Content/sweetalert2.min.css");
                    return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                   "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                   "<script>swal({title: 'ERROR..'," +
                                   "text: 'El contrato que deseas registrar ya existe en la base de datos.'," +
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

            ViewBag.Clienteid= new SelectList(db.Cliente, "id", "NombreCliente", contrato.Clienteid);
            return View(contrato);
        }

       

        // GET: Contratos/Edit/
        [CustomAuth(Roles = "Monitor, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El contrato que deseas editar no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            
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
            ViewBag.Clienteid = new SelectList(db.Cliente, "id", "NombreCliente", contrato.Clienteid);
            return View(contrato);
        }

        // POST: Contratos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CodigoContrato,FechaInicioContrato,FechaFinContrato,Clienteid,ValorContrato,EstadoContrato")] Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                try {
                    var ValDateEnd = db.Prorroga.Where(x => x.Contratoid == contrato.id).FirstOrDefault();

                    if (ValDateEnd != null && contrato.FechaFinContrato == null)
                    {
                        string hh = Url.Content("~/Content/sweetalert2.min.css");
                        return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                       "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                       "<script>swal({title: 'ERROR..'," +
                                       "text: 'No puedes editar este contrato con fecha fin nula porque tiene una o varias prórrogas.'," +
                                       "type: 'error'," +
                                       "showCancelButton: false," +
                                       "confirmButtonColor: '#3085d6'," +
                                       "cancelButtonColor: '#d33'," +
                                       "confirmButtonText: 'Aceptar!'}).then(function() " +
                                       "{swal(''," +
                                       "''," +
                                       "'success', window.location.href='/Contratos/Index')});</script>");
                    }
               
                    else
                    {
                        
                        var RepeatName = db.Contrato.AsNoTracking()
                            .Where(x => x.Clienteid == contrato.Clienteid && x.CodigoContrato == contrato.CodigoContrato && x.id != contrato.id)
                            .FirstOrDefault();

                        if (RepeatName == null)
                        {

                            db.Entry(contrato).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");

                        }
                        else {
                            string hh = Url.Content("~/Content/sweetalert2.min.css");
                            return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                                           "<script src='/Scripts/sweetalert2.min.js'></script>." +
                                           "<script>swal({title: 'ERROR..'," +
                                           "text: 'El contrato que deseas editar ya existe para este cliente.'," +
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
                }catch (Exception){

                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<script>swal({title: 'ERROR..'," +
                               "text: 'El contrato que deseas registrar ya existe en la base de datos.'," +
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
            ViewBag.Clienteid = new SelectList(db.Cliente, "id", "NombreCliente", contrato.Clienteid);
            return View(contrato);
        }

        // GET: Contratos/Delete/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'Petición incorrecta al servidor..'," +
                               "text: 'El contrato que desea eliminar no existe en la base de datos, debes intentarlo de nuevo.'," +
                               "type: 'error'," +
                               "showCancelButton: false," +
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33'," +
                               "confirmButtonText: 'Aceptar!'}).then(function() " +
                               "{swal(''," +
                               "''," +
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
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
            return View(contrato);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contrato contrato = db.Contrato.Find(id);
            db.Contrato.Remove(contrato);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                string hh = Url.Content("~/Content/sweetalert2.min.css");
                return Content("<link href='" + hh + "' rel='stylesheet' type='text/css'/>" +
                               "<script src='/Scripts/sweetalert2.min.js'></script>." +
                               "<Script>swal({title: 'No puedes realizar esta acción..',"+
                               "text: 'Este contrato tiene una póliza o varias prórrogas asociadas asegúrese de borrar esas dependencias para proceder a eliminar este registro.',"+
                               "type: 'error',"+
                               "showCancelButton: false,"+
                               "confirmButtonColor: '#3085d6'," +
                               "cancelButtonColor: '#d33',"+
                               "confirmButtonText: 'Aceptar!'}).then(function() "+
                               "{swal('Hecho!',"+
                               "'',"+
                               "'success', window.location.href='/Contratos/Index')});</script>");
            }
            return RedirectToAction("Index");
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
