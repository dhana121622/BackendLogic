using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class UserLogin
    {
        
        [Required]   
        public string UserName { get; set; } = string.Empty;


        [Required]
        [Range(6666666666,9999999999,ErrorMessage ="Please Enter a Valid PhoneNumber")]
        public long PhoneNumber { get; set; }


        [Required]
        [Key]
        [EmailAddress(ErrorMessage ="Please Enter a Valid Mail ID")]
        public string Email { get; set; } = string.Empty;



        [Required]
        [StringLength(10,MinimumLength =6,ErrorMessage ="Enter Above 6 Character")]
        public string Password { get; set; } = string.Empty;



        [Required]
        [Compare("Password",ErrorMessage ="Password Not Match")]
        public string ConfirmPassword { get; set; } = string.Empty;


        [Required]
        public string Gender { get; set; } = string.Empty;


        [Required]
        public DateOnly date { get; set; }


        public byte[]? Profile_Image { get; set; }

    }
}
