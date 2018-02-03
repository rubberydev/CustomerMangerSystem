using Ingeneo.AppContratos.Models;
using Ingeneo.AppContratos.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ingeneo.AppContratos.Controllers.API
{
    public class PolizasController : ApiController
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();
        public DateTime FirstN = DateTime.Today.AddDays(10);
        
        // GET: api/Polizas
        public List<Notification> Get()
        {
            var polizaN = db.Poliza.ToList();
            var polizasNView = new List<Notification>();
            foreach (var poliza in polizaN)
            {
                if (poliza.FechaFinpoliza != null)
                {
                    var DateN = poliza.FechaFinpoliza;
                    if (FirstN.Year == DateN.Year && FirstN.Month == DateN.Month && FirstN.Day == DateN.Day)
                    {
                        if (poliza.idContrato != null)
                        {
                            var polizaNView = new Notification
                            {
                                id = poliza.Contrato.id,
                                NombreAseguradora = poliza.NombreAseguradora,
                                CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaInicioPoliza,
                                FechaFin = poliza.FechaFinpoliza,
                            };
                            polizasNView.Add(polizaNView);
                        }
                        else
                        {
                            var polizaNView = new Notification
                            {
                                id = poliza.Prorroga.id,
                                NombreAseguradora = poliza.NombreAseguradora,
                                //CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaInicioPoliza,
                                FechaFin = poliza.FechaFinpoliza,
                            };
                            polizasNView.Add(polizaNView);
                        }                       
                    }
                }
            }
            return polizasNView;
        }

        //// GET: api/Polizas/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Polizas
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Polizas/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Polizas/5
        //public void Delete(int id)
        //{
        //}
    }
}
