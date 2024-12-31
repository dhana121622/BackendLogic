using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Group_Chat
    {
        [Key]
        public int Id { get; set; }

        public string? GroupName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

    }
}
