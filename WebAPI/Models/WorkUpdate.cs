using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class WorkUpdate
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }   
        public DateOnly date {  get; set; } 
        public string WorkUpdates { get; set; }
        public string TaskLinks { get; set; }
        public string statusmessage { get; set; }
        public string feedbackmessage { get; set; }
    }
}
