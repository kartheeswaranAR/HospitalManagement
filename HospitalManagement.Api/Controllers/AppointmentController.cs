using AutoMapper;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Context;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointment;
        private readonly IMapper _mapper;
        public AppointmentController(IAppointmentRepository appointment, IMapper mapper)
        {
            _appointment = appointment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AppointmentEntity>> ViewAppointments()
        {
            try
            {
                var appoint = await _appointment.GetAppointments();
                var AppointmentEntity = _mapper.Map<List<AppointmentEntity>>(appoint);
                return Ok(AppointmentEntity);
            }
            catch(Exception e)
            {
                throw new Exception("No Appointments are there currently !", e);
            }
        }

        [HttpGet("{Token}")]
        public Task<Appointment> ViewAppointmentByID(Guid Token)
        {
            try
            {
                return _appointment.GetAppointmentByID(Token);
            }
            catch(Exception e)
            {
                throw new Exception("No Appointments are there for current ID", e);
            }
        }

       
        [HttpPost]
        public Task<Appointment> AddAppointment(Appointment addNew)
        {
            try
            {
                return _appointment.AddApointment(addNew);
            }
            catch(Exception e)
            {
                throw new Exception("Please Fill all details!!", e);
            }
        }

      
        [HttpDelete("{Id}")]
        public Task<Appointment> DeleteAppointment(Guid Token)
        {
            try
            {
                return _appointment.DeleteAppointment(Token);
            }
            catch(Exception e)
            {
                throw new Exception("Please given correct ID !", e);
            }
        }
    }
}
