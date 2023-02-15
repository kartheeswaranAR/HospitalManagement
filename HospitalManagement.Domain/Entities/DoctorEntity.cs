using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Entities
{
    public class DoctorEntity
    {
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int? Experience { get; set; }
        public string? Specialization { get; set; }

    }
}
