using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ingeneo.AppContratos.ModelViews
{
    public class ContratoNotification
    {
        public int ContratoID { get; set; }

        public string CodigoContrato { get; set; }


        [NotMapped]
        //[Display(Name = "Fecha Inicio de contrato")]
        ////[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaInicioContrato { get; set; }

        [NotMapped]
        //[Display(Name = "Fecha fin de contrato")]
        ////[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public Nullable<System.DateTime> FechaFinContrato { get; set; }

    }
}