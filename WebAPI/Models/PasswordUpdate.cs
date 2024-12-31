using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PasswordUpdate
    {
        public string Email { get; set; } = string.Empty;

        [StringLength(10, MinimumLength = 6, ErrorMessage = "Enter Above 6 Character")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
