using HospitalManagement.Domain.Context;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DBContext _context;
        public DoctorRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<Doctor> AddDoctor(Doctor addDoctor)
        {
            _context.DoctorModel.Add(addDoctor);
            await _context.SaveChangesAsync();
            return addDoctor;
        }

        public async Task<Doctor> DeleteDoctor(Guid id)
        {
            var removeDoctor = await _context.DoctorModel.FirstOrDefaultAsync(x => x.Id == id);
            _context.Remove(removeDoctor);
            await _context.SaveChangesAsync();
            return removeDoctor;
        }

        public async Task<Doctor> EditDoctor(Guid id, Doctor editDoctor)
        {
            var updatedoctor = await _context.DoctorModel.FirstOrDefaultAsync(n => n.Id == id);
            updatedoctor.Name = editDoctor.Name;
            updatedoctor.DOB = editDoctor.DOB;
            updatedoctor.Experience = editDoctor.Experience;
            updatedoctor.Specialization = editDoctor.Specialization;
            updatedoctor.MobileNumber = editDoctor.MobileNumber;
            updatedoctor.Email = editDoctor.Email;
            await _context.SaveChangesAsync();
            return editDoctor;
        }

        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await _context.DoctorModel.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByID(Guid Id)
        {
            return await _context.DoctorModel.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
