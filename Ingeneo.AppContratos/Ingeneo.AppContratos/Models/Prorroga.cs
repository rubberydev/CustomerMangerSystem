using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingeneo.AppContratos.Models
{
    [Table("Prorroga")]
    public class Prorroga
    {
        public int id { set; get; }

        [Required(ErrorMessage = "La fecha inicio de la prórroga es un campo obligatorio")]
        [Display(Name = "Fecha inicio de la prórroga")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaInicioProrroga{set; get;}

        [Display(Name = "Fecha fin de la prórroga")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime? FechaFinProrroga { set; get; }

        public int? Contratoid { set; get; }

        public virtual Contrato Contrato { set; get; }

        public virtual ICollection<Poliza> PolizaPro { set; get; }

    }
}