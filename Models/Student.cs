using System.ComponentModel.DataAnnotations;

namespace CoachingWebsite.Models
{
    public class Student
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }


        [Required]
        public string Course { get; set; }


        public string? ImagePath { get; set; }


        public bool IsPresent { get; set; }


        public bool FeesPaid { get; set; }
    }
}