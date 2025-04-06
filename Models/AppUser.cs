using Microsoft.AspNetCore.Identity;


namespace BeFit.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
