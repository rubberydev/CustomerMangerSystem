using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ingeneo.AppContratos.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Recordar este Navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene formato valido")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La {0} debe ser minimo de 6 caracteres y maximo 20.")]
        [DataType(DataType.Password)]        
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recuerdame?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene formato valido")]       
        [RegularExpression(@"[a-zA-Z0-9.-_\\-]+@[a-zA-Z0-9.-_]+\.[a-zA-Z]{2,}", ErrorMessage = "El {0} debe tener la siguiente estructura: micorreo@midominio.com y solo permite los siguientes caracteres especiales: (punto: . , guion medio: - , guion bajo: _ ) ")]
        [Display(Name = "Email")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El {0} debe ser minimo de 5 caracteres y maximo 30, e.g: micorreo@midominio.com")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La {0} debe ser minimo de 6 caracteres y maximo 20.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmacion son diferentes.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene formato valido")]
        [RegularExpression(@"[a-zA-Z0-9.-_]+@[a-zA-Z0-9.-_]+\.[a-zA-Z]{2,}", ErrorMessage = "El campo {0} solo permite los siguientes caracteres especiales: (punto: . , guion medio: - , guion bajo: _ ) ")]
        [Display(Name = "Email")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El {0} debe ser minimo de 5 caracteres y maximo 30, e.g: micorreo@midominio.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La {0} debe ser minimo de 6 caracteres y maximo 20.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmacion son diferentes.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene formato valido")]
        [Display(Name = "Email")]        
        public string Email { get; set; }
    }
}
