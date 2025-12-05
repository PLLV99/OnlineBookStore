using System.ComponentModel.DataAnnotations;
namespace OnlineBookStore.ViewModels
{
    public class RegisterVM
    {
        [Required] public string Name { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required][DataType(DataType.Password)] public string Password { get; set; }
        [Compare("Password")][DataType(DataType.Password)] public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}