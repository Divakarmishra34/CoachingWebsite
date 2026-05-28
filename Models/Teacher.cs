using System.ComponentModel.DataAnnotations;

namespace CoachingWebsite.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Email { get; set; }
    }
}