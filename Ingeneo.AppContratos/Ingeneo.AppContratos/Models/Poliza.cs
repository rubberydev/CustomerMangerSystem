using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ingeneo.AppContratos.Models
{
    [Table("Poliza")]
    public class Poliza
    {
        public Poliza()
        {
            this.DetallePoliza = new HashSet<DetallePoliza>();
        }

        public int id { set; get; }

        [Index(IsUnique = true)]
        [StringLength(30,MinimumLength = 2,ErrorMessage ="El código de la póliza debe ser minimo de dos caracteres y maximo 30 y alfanumérico")]
        [Required(ErrorMessage ="Código de la póliza es un campo obligatorio")]
        [Display(Name = "Código de la Póliza")]
        public string CodigoPoliza { get; set; }

        [Required(ErrorMessage = "El nombre de la aseguradora es un campo obligatorio")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Este campo debe contener minimo 3 caracteres y maximo 50")]
        public string NombreAseguradora { set; get; }        

        [Required(ErrorMessage ="La fecha inicio de la póliza es un campo obligatorio")]
        [Display(Name = "Fecha inicio de la Póliza")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaInicioPoliza { set; get; }

        [Required(ErrorMessage = "La fecha fin de la póliza es un campo obligatorio")]
        [Display(Name = "Fecha fin de la Póliza")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00:yyyy/MM/dd}")]
        public DateTime FechaFinpoliza { set; get; }
        
            
        public int? idContrato { set; get; }
        public int? idProrroga { set; get; }

        public virtual Prorroga Prorroga { set; get; }
        public virtual Contrato Contrato { set; get; }
        public virtual ICollection<DetallePoliza> DetallePoliza { set; get; }
      
    }
}