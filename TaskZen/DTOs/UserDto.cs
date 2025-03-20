using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskZen.Models;

namespace TaskZen.DTOs
{
    public class UserDto
    {

        [Required]
        [Length(1, 50)]
        public string Name { get; set; }

        [Length(1, 50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [CustomValidation(typeof(User), "ValidateDateBirth")]
        public DateOnly DateBirth { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
