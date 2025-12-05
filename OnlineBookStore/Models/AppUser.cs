using Microsoft.AspNetCore.Identity;
namespace OnlineBookStore.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}