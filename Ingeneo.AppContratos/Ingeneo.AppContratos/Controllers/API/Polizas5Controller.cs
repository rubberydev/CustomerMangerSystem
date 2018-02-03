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
    public class Polizas5Controller : ApiController
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();
        public DateTime SecN = DateTime.Today.AddDays(5);
        // GET: api/Polizas5
        public List<Notification> Get()
        {
            var polizaN = db.Poliza.ToList();
            var polizasN5View = new List<Notification>();
            foreach (var poliza in polizaN)
            {
                if (poliza.FechaFinpoliza != null)
                {
                    var DateN = poliza.FechaFinpoliza;
                    if (SecN.Year == DateN.Year && SecN.Month == DateN.Month && SecN.Day == DateN.Day)
                    {
                        if (poliza.idContrato != null)
                        {
                            var polizaN5View = new Notification
                            {
                                id = poliza.Contrato.id,
                                NombreAseguradora = poliza.NombreAseguradora,
                                CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaInicioPoliza,
                                FechaFin = poliza.FechaFinpoliza,
                            };
                            polizasN5View.Add(polizaN5View);
                        }
                        else
                        {
                            var polizaN5View = new Notification
                            {
                                id = poliza.Prorroga.id,
                                NombreAseguradora = poliza.NombreAseguradora,
                                //CodigoContrato = poliza.Contrato.CodigoContrato,
                                FechaInicio = poliza.FechaInicioPoliza,
                                FechaFin = poliza.FechaFinpoliza,
                            };
                            polizasN5View.Add(polizaN5View);
                        }                                              
                    }
                }
            }
            return polizasN5View;
        }

        //// GET: api/Polizas5/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Polizas5
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Polizas5/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Polizas5/5
        //public void Delete(int id)
        //{
        //}
    }
}
