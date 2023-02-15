using HospitalManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctors();
        Task<Doctor> GetDoctorByID(Guid Id);
        Task<Doctor> AddDoctor(Doctor addDoctor);
        Task<Doctor> EditDoctor(Guid id, Doctor editDoctor);
        Task<Doctor> DeleteDoctor(Guid id);
    }
}
