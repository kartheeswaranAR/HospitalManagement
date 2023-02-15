using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Domain.Models
{
    public class Appointment
    {
        [Key]
        [Required]
        public Guid Token { get; set; }= Guid.NewGuid();

        [Required]
        public string? DoctorName { get; set; }

        [Required]
        public string? PatientName { get; set; }
        public string? Description { get; set; }

        [Required]
        public DateTime? AppointmentDate { get; set; } = DateTime.Now;
        public bool isPaid { get; set; }
    }
}
