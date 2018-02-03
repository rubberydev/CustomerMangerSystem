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
    public class Contrato5Controller : ApiController
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();
        public DateTime FirstN = DateTime.Today.AddDays(10);
        public DateTime SecN = DateTime.Today.AddDays(5);

        // GET: api/Contrato5
        public List<Notification> Get()
        {
            var contratosN5View = new List<Notification>();
            var contratosN = db.Contrato.ToList();
            foreach (var contrato in contratosN)
            {
                if (contrato.FechaFinContrato.HasValue)
                {
                    var DateN = contrato.FechaFinContrato.Value;
                    if (SecN.Year == DateN.Year && SecN.Month == DateN.Month && SecN.Day == DateN.Day)
                    {
                        var contratoN5View = new Notification
                        {
                            id = contrato.id,
                            NombreCliente = contrato.Cliente.NombreCliente,
                            CodigoContrato = contrato.CodigoContrato,
                            ValorContrato = contrato.ValorContrato,
                            FechaInicio = contrato.FechaInicioContrato,
                            FechaFin = contrato.FechaFinContrato
                        };
                        contratosN5View.Add(contratoN5View);                        
                    }
                }
            }
            return contratosN5View;
        }

        // GET: api/Contrato5/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Contrato5
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Contrato5/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Contrato5/5
        //public void Delete(int id)
        //{
        //}
    }
}
