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
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctor;
        private readonly IMapper _mapper;
        public DoctorController(DBContext DBcontext,IDoctorRepository doctor,IMapper mapper)
        {
            _doctor = doctor;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DoctorEntity>> ViewDoctors()
        {
            try
            {
                var patients = await _doctor.GetAllDoctors();
                var doctorEntity = _mapper.Map<List<DoctorEntity>>(patients);
                return Ok(doctorEntity);
            }
            catch(Exception e)
            {
                throw new Exception("No Doctors are enrolled..", e);
            }
        }

        [HttpGet("{Id}")]
        public async Task<Doctor> ViewDoctor(Guid Id)
        {
            try
            {
                return await _doctor.GetDoctorByID(Id);
            }
            catch(Exception e)
            {
                throw new Exception("The User is not Found.",e);
            }
        }
        [HttpPost]
        public Task<Doctor> AddDoctor(Doctor doctor)
        {
            try
            {
                return _doctor.AddDoctor(doctor);
            }
            catch(Exception e)
            {
                var statuscode = new HttpResponseMessage(System.Net.HttpStatusCode.NotImplemented);
                throw new Exception("User is not added",e);
            }
        }


        [HttpPut("{Id}")]
        public  Task<Doctor> EditPatient(Guid Id, Doctor doctor)
        {
            try
            {
                return _doctor.EditDoctor(Id, doctor);
            }
            catch(Exception e)
            {
                throw new Exception("The Given user is not found or not editable",e);
            }
        }

        [HttpDelete("{Id}")]
        public Task<Doctor> DeletePatient(Guid Id)
        {
            try
            {
                return _doctor.DeleteDoctor(Id);
            }
            catch(Exception e)
            {
                throw new Exception("Please check the ID",e);
            }
        }
    }
}
