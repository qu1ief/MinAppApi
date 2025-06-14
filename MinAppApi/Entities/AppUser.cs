using Microsoft.AspNetCore.Identity;

namespace MinAppApi.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
