using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class AdminLogin
    {
        public string UserName { get; set; }
        [Key]
        public string Email {  get; set; }
        public string Password { get; set; }
    }
}
