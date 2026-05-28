using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoachingWebsite.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
        [NotMapped]

public string ConfirmPassword { get; set; }
    }
    
}