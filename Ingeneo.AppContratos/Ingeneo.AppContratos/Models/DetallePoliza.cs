using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingeneo.AppContratos.Models
{
    public class DetallePoliza
    {
    
        public int id { set; get; }
        
        [Display(Name ="Descripción cobertura")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El minimo de caracteres introducidos debe ser 2 y maximo 50")]
        public string DescripcionCobertura { set; get; }

        [Required(ErrorMessage = "La fecha inicio de la cobertura es un campo obligatorio")]
        [Display(Name = "Fecha inicio de la cobertura")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaInicioProteccion {set; get;}

        [Required(ErrorMessage ="La fecha fin de la cobertura es un campo obligatorio")]
        [Display(Name = "Fecha fin de la cobertura")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaFinProteccion { set; get; }
        public int PolizaId { set; get; }
        [ForeignKey("PolizaId")]
        public virtual Poliza Poliza { get; set; }

    }  
}