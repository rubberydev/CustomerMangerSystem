using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity;

namespace Ingeneo.AppContratos.Models
{

    [Table("Cliente")]
    public class Cliente
    {
        public Cliente()
        {
            this.Contrato = new HashSet<Contrato>();
        }
        public int id { set; get; }

             
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El Nit del cliente debe ser minimo de 3 caracteres y maximo 30 y alfanumérico")]
        [Required(ErrorMessage = "El Nit del cliente es requerido")]
        public string Nit { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(60, ErrorMessage = "El nombre del cliente debe ser minimo de 3 caracteres y maximo 60")]
        [MinLength(3)]
        //[StringLength(MinimumLength = 3, ErrorMessage = "El Nit del cliente debe ser minimo de 3 caracteres y maximo 30 y alfanumérico")]
        [Display(Name ="Nombre del Cliente")]
        [Required(ErrorMessage = "El Nombre del cliente es requerido")]
        public string NombreCliente { set; get; }

        [Display(Name = "País")]
        public string Pais { set; get; }
             
        public string Ciudad { set; get; }

        [Display(Name ="Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "El minimo de caracteres es 7 maximo 20")]
        [Phone(ErrorMessage ="Este campo debe contener un numero de telefono valido no se permiten caracteres especiales, tampoco letras")]
        public string Telefono { set; get; }
        
        [Display(Name ="Dirección")]
        //[StringLength(100, MinimumLength = 5, ErrorMessage = "El minimo de caracteres es 5 maximo 150")]
        [MaxLength(150, ErrorMessage = "El minimo de caracteres es 5 y  maximo 150")]
        [MinLength(5)]
        [DataType(DataType.MultilineText)]
        public string Direccion { set; get; }
         
        [DataType(DataType.EmailAddress)]        
        [EmailAddress(ErrorMessage = "El campo Email no es una dirección de correo electrónico válida e.g micorreo@dominio.com")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "El minimo de caracteres es 5 maximo 100")]
        public string Email { set; get; }


        //[Url(ErrorMessage = "El campo URL no es una dirección URL http, https o ftp completa no se permiten caracteres especiales.")]         
        [StringLength(80, MinimumLength = 5, ErrorMessage = "El minimo de caracteres es 5 y maximo 80")]
        public string URL { set; get; }
        public virtual ICollection<Contrato> Contrato { set; get; }
    }
}