using Microsoft.AspNetCore.Identity;

namespace MitigatingCircumstances.Models
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}
