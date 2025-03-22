using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskZen.Models
{
    [Table("User")]
    public class User
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

        public static ValidationResult ValidateDateBirth(object value)
        {
            if (value == null)
            {
                return new ValidationResult("La fecha de nacimiento es obligatoria.");
            }

            if (value is not DateOnly dateBirth)
            {
                return new ValidationResult("El valor proporcionado no es una fecha válida.");
            }

            if (dateBirth > DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("La fecha de nacimiento no puede ser mayor a la fecha actual.");
            }

            return ValidationResult.Success;
        }

    }
}
