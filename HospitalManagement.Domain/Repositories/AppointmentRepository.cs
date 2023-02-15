using HospitalManagement.Domain.Entities;
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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DBContext _context;
        
        public AppointmentRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<Appointment> AddApointment(Appointment addAppointment)
        {
            _context.AppointmentModel.Add(addAppointment);
            await _context.SaveChangesAsync();
            return addAppointment;
        }

        public async Task<Appointment> DeleteAppointment(Guid Token)
        {
            var removeAppointment = await _context.AppointmentModel.FirstOrDefaultAsync(x => x.Token == Token);
            _context.Remove(removeAppointment);
            await _context.SaveChangesAsync();
            return removeAppointment;
        }

        public async Task<Appointment> GetAppointmentByID(Guid Token)
        {
            return await _context.AppointmentModel.FirstOrDefaultAsync(x => x.Token == Token);
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            return await _context.AppointmentModel.ToListAsync();
        }
    }
}
