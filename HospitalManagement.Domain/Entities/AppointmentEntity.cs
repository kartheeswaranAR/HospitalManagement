using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Entities
{
    public class AppointmentEntity
    {
        public Guid Token { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public bool isPaid { get; set; }
    }
}
