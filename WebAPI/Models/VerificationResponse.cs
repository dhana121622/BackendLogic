using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class VerificationResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //[StringLength(10, MinimumLength = 6, ErrorMessage = "Enter Above 6 Character")]
        public string Password { get; set; } = string.Empty;

        //[Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateOnly date { get; set; }

        public byte[]? Profile_Image { get; set; }
    }
}
