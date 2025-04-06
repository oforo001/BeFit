using System.ComponentModel.DataAnnotations;

namespace BeFit.ViewModels
{
    public class LoginViewModelcs
    {
        [Required(ErrorMessage = "Email is requred.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requred.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me.")]
        public bool RememberMe { get; set; }
    }
}
