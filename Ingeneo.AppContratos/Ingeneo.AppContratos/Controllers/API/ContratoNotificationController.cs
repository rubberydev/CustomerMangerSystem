using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ingeneo.AppContratos.Models;
using Ingeneo.AppContratos.ModelViews;

namespace Ingeneo.AppContratos.Controllers.API
{
    public class ContratoNotificationController : ApiController
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();

        public DateTime FirstN = DateTime.Today.AddDays(10);
        public DateTime secondN = DateTime.Today.AddDays(5);

        //*********metodo para buscar entre todos los contratos y agruparlos en *******
        //*********una lista 10 dias antes de que terminen la fecha final de contrato**
        /*      GET: api/ContratoNotification       */
        public List<Notification> GetContrato()
        {
            var contratosN = db.Contrato.ToList();
            var contratosNView = new List<Notification>();

            foreach (var contrato in contratosN)
            {
                if (contrato.FechaFinContrato.HasValue)
                {
                    var DateN = contrato.FechaFinContrato.Value;
                    if (FirstN.Year == DateN.Year && FirstN.Month == DateN.Month && FirstN.Day == DateN.Day)
                    {
                        var contratoNView = new Notification
                        {
                            id = contrato.id,
                            NombreCliente = contrato.Cliente.NombreCliente,
                            CodigoContrato = contrato.CodigoContrato,
                            ValorContrato = contrato.ValorContrato,
                            FechaInicio = contrato.FechaInicioContrato,
                            FechaFin = contrato.FechaFinContrato,                            
                            //ContratoID = contrato.id,
                            //CodigoContrato = contrato.CodigoContrato,
                            //FechaInicioContrato = contrato.FechaInicioContrato,
                            //FechaFinContrato = contrato.FechaFinContrato,
                        };
                        contratosNView.Add(contratoNView);
                    }
                }
            }
            return contratosNView;
        }



















        

        // GET: api/ContratoNotification/5
        [ResponseType(typeof(Contrato))]
        public IHttpActionResult GetContrato(int id)
        {
            Contrato contrato = db.Contrato.Find(id);
            if (contrato == null)
            {
                return NotFound();
            }

            return Ok(contrato);
        }

        // PUT: api/ContratoNotification/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContrato(int id, Contrato contrato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contrato.id)
            {
                return BadRequest();
            }

            db.Entry(contrato).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ContratoNotification
        [ResponseType(typeof(Contrato))]
        public IHttpActionResult PostContrato(Contrato contrato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contrato.Add(contrato);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contrato.id }, contrato);
        }

        // DELETE: api/ContratoNotification/5
        [ResponseType(typeof(Contrato))]
        public IHttpActionResult DeleteContrato(int id)
        {
            Contrato contrato = db.Contrato.Find(id);
            if (contrato == null)
            {
                return NotFound();
            }

            db.Contrato.Remove(contrato);
            db.SaveChanges();

            return Ok(contrato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContratoExists(int id)
        {
            return db.Contrato.Count(e => e.id == id) > 0;
        }
    }
}