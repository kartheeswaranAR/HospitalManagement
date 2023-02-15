using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Domain.Models
{
    public class Patient
    {
        [Key]
        [Required]
        public Guid Id { get; set; } =Guid.NewGuid();

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime? DOB { get; set; } 
        public string ? Gender { get; set; }
        public string ? Address { get; set; }

        [Required]
        public long? MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Description { get; set;}

        [PasswordPropertyText]
        [Required]
        public string ? Password { get; set; }
    }
}
