using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Context;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DBContext _context;
        public PatientRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Patient> AddPatient(Patient addPatient)
        {
            _context.PatientModel.AddAsync(addPatient);
            await _context.SaveChangesAsync();
            return addPatient;
        }

        public async Task<Patient> DeletePatient(Guid id)
        {
            var removePatient = await _context.PatientModel.FirstOrDefaultAsync(x=>x.Id==id);
            _context.Remove(removePatient);
            await _context.SaveChangesAsync();
            return removePatient;
        }

        public async Task<Patient> EditPatient(Guid id, Patient editPatient)
        {
            var updatePatient = await _context.PatientModel.FirstOrDefaultAsync(n=>n.Id==id);
            updatePatient.Name = editPatient.Name;
            updatePatient.MobileNumber = editPatient.MobileNumber;
            updatePatient.DOB = editPatient.DOB;
            updatePatient.Gender = editPatient.Gender;
            updatePatient.Address = editPatient.Address;
            updatePatient.MobileNumber = editPatient.MobileNumber;
            updatePatient.Email = editPatient.Email;
            updatePatient.Description = editPatient.Description;
            await _context.SaveChangesAsync();
            return editPatient;

        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _context.PatientModel.ToListAsync();
        }

        public async Task<Patient> GetPatientByID(Guid Id)
        {
            return await _context.PatientModel.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
