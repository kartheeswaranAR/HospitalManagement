using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Entities
{
    public class PatientEntity
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DOB { get; set; } 

        [EmailAddress]
        public string? Email { get; set; }

        public string? Description { get; set; }
    }
}
