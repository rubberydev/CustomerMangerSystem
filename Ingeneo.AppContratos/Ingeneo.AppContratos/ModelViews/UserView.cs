using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ingeneo.AppContratos.ModelViews
{
    public class UserView
    {
        public string UserID { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-zA-Z0-9.-_]+@[a-zA-Z0-9.-_]+\.[a-zA-Z]{2,}", ErrorMessage = "El campo {0} solo permite los siguientes caracteres especiales: (punto: . , guion medio: - , guion bajo: _ ) ")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene formato valido")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El {0} debe ser minimo de 5 caracteres y maximo 30, e.g: micorreo@midominio.com")]
        public string Email { get; set; }

        public RoleView Role { get; set; }

        public List<RoleView> Roles { get; set; }
    }
}