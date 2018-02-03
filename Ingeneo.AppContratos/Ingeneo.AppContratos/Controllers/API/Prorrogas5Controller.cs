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
    public class Prorrogas5Controller : ApiController
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();        
        public DateTime SecN = DateTime.Today.AddDays(5);
        // GET: api/Prorrogas5
        public List<Notification> Get()
        {
            var prorrogaN = db.Prorroga.ToList();
            var prorrogasN5View = new List<Notification>();
            foreach (var prorroga in prorrogaN)
            {
                if (prorroga.FechaFinProrroga.HasValue)
                {
                    var DateN = prorroga.FechaFinProrroga.Value;
                    if (SecN.Year == DateN.Year && SecN.Month == DateN.Month && SecN.Day == DateN.Day)
                    {
                        var prorrogaN5View = new Notification
                        {
                            id = prorroga.Contrato.id,
                            CodigoContrato = prorroga.Contrato.CodigoContrato,
                            FechaInicio = prorroga.FechaInicioProrroga,
                            FechaFin = prorroga.FechaFinProrroga,
                        };
                        prorrogasN5View.Add(prorrogaN5View);                        
                    }
                }
            }
            return prorrogasN5View;
        }

        //GET: api/Prorrogas5/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //POST: api/Prorrogas5
        //public void Post([FromBody]string value)
        //{
        //}

        //PUT: api/Prorrogas5/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //DELETE: api/Prorrogas5/5
        //public void Delete(int id)
        //{
        //}
    }
}
