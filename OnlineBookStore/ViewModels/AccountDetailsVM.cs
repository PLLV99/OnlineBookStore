using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.ViewModels
{
    public class AccountDetailsVM
    {
        public string? Email { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}


