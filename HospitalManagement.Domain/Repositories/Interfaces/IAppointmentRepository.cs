using HospitalManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointments();
        Task<Appointment> GetAppointmentByID(Guid Token);
        Task<Appointment> AddApointment(Appointment addAppointment);
        Task<Appointment> DeleteAppointment(Guid Token);
    }
}
