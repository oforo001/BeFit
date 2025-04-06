using System.ComponentModel.DataAnnotations;

namespace BeFit.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is requered.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
