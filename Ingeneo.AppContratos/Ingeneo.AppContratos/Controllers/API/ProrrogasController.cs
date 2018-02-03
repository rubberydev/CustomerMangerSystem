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
    public class ProrrogasController : ApiController
    {
        private GestorContratosDbContext db = new GestorContratosDbContext();
        public DateTime FirstN = DateTime.Today.AddDays(10);        
        // GET: api/Prorrogas
        public List<Notification> Get()
        {
            var prorrogaN = db.Prorroga.ToList();
            var prorrogasNView = new List<Notification>();
            foreach (var prorroga in prorrogaN)
            {
                if (prorroga.FechaFinProrroga.HasValue)
                {
                    var DateN = prorroga.FechaFinProrroga.Value;
                    if (FirstN.Year == DateN.Year && FirstN.Month == DateN.Month && FirstN.Day == DateN.Day)
                    {
                        var prorrogaNView = new Notification
                        {
                            id = prorroga.Contrato.id,
                            CodigoContrato = prorroga.Contrato.CodigoContrato,
                            FechaInicio = prorroga.FechaInicioProrroga,
                            FechaFin = prorroga.FechaFinProrroga
                        };
                        prorrogasNView.Add(prorrogaNView);                        
                    }
                }
            }
            return prorrogasNView;
        }       
    }
}
