using Ingeneo.AppContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ingeneo.AppContratos.ModelViews
{
    public class ProrrogaContrato
    {
        public Prorroga prorroga { get; set; }

        public Contrato contrato { get; set; }
    }
}