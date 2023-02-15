using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Domain.Models
{
    public class Doctor
    {
        [Key]
        [Required]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime? DOB { get; set; }

        [Required]
        public int? Experience { get; set;}

        [Required]
        public string? Specialization { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public long? MobileNumber { get; set; }

        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
