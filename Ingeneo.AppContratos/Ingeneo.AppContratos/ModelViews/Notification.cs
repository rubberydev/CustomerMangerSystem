using Ingeneo.AppContratos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ingeneo.AppContratos.ModelViews
{
    [NotMapped]
    public class Notification
    {
        public int id { get; set; }

        [NotMapped]
        [Index(IsUnique = true)]
        //[Required(ErrorMessage = "El nombre del contrato es requerido")]
        [Display(Name = "Nombre del contrato")]
        //[StringLength(20, MinimumLength = 2, ErrorMessage = "El minimo de caracteres introducidos debe ser 2 y maximo 20 y alfanumérico")]
        public string CodigoContrato { get; set; }

        [NotMapped]
        [Display(Name = "Nombre de la Aseguradora")]
        public string NombreAseguradora { set; get; }

        [NotMapped]
        [Display(Name = "Nombre de la Aseguradora")]
        public string NombreCliente{ set; get; }

        [NotMapped]
        [Display(Name = "Valor del contrato")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal? ValorContrato { get; set; }

        [NotMapped]
        //[Required(ErrorMessage = "La fecha inicio del contrato es requerida")]
        [Display(Name = "Fecha Inicio de contrato")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaInicio { get; set; }

        [NotMapped]
        [Display(Name = "Fecha fin de contrato")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public Nullable<System.DateTime> FechaFin { get; set; }

        public virtual Cliente Cliente { set; get; }

        public virtual Contrato Contrato { get; set; }

        public virtual Prorroga Prorroga { get; set; }
        public virtual Poliza Poliza { get; set; }

    }
}