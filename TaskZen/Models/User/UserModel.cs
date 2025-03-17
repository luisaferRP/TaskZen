using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskZen.Models.User
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("date_birth")]
        public DateOnly DateBirth { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }
    }
}
