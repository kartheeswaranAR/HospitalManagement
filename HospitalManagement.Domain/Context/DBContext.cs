using HospitalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Context
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Patient> PatientModel { get; set; }
        public DbSet<Doctor> DoctorModel { get; set; }
        public DbSet<Appointment> AppointmentModel { get; set; }
       
    }
}
