using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingeneo.AppContratos.Models
{
    [Table("Contrato")]
    public class Contrato
    {
        public int id { get; set; }

        [Display(Name = "Valor del contrato")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "El campo valor del contrato solo permite valores numericos sin puntos y/o comas.")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]                    
        public decimal? ValorContrato { get; set; }

        
        [Required(ErrorMessage = "El nombre del contrato es requerido")]
        [Display(Name = "Nombre del contrato")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "El minimo de caracteres introducidos debe ser 2 y maximo 30 y alfanumérico")]
        public string CodigoContrato { get; set; }

        [Required(ErrorMessage ="La fecha inicio del contrato es requerida")]
        [Display(Name = "Fecha Inicio de contrato")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaInicioContrato { get; set; }

        [Display(Name = "Fecha fin de contrato")]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]

        public Nullable<System.DateTime> FechaFinContrato { get; set; }

        [Display(Name ="Inactivo")]
        public Boolean EstadoContrato { get; set; }
        //Puede ser igual al nombre del id en la entidad Cliente y no tendria que especificarse en el DbContext

        [Display(Name = "Cliente")]
        public int? Clienteid { set; get; }
        public virtual Cliente Cliente { set; get; }
        public virtual ICollection<Prorroga> Prorroga { set; get; }
        public virtual ICollection<Poliza> Poliza { set; get; }
        //Conventions codeFirst

      
    }
}