using HospitalManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatients();
        Task<Patient> GetPatientByID(Guid Id);
        Task<Patient> AddPatient(Patient addPatient);
        Task<Patient> EditPatient(Guid id,Patient editPatient);
        Task<Patient> DeletePatient(Guid id);
    }
}
