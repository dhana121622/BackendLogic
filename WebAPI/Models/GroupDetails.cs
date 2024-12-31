using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class GroupDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<string> UserName { get; set; }

    }
}
