using AutoMapper;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Repositories;
using HospitalManagement.Domain.Context;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using HospitalManagement.Api.Handler;

namespace HospitalManagement.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patient;
        private readonly IMapper _mapper;
        public PatientController(IPatientRepository patient, IMapper mapper)
        {
            _patient = patient;
            _mapper = mapper;
        }

        /// <summary>
        /// View all the patient who got admission
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [HttpGet]
        public async Task<ActionResult<List<PatientEntity>>> ViewAllPatients()
        {
            try
            {
                var patients = await _patient.GetAllPatients();
                var patientEntity = _mapper.Map<List<PatientEntity>>(patients);
                return Ok(patientEntity);
                
            }
            catch(Exception)
            {
                throw new Exception("No Patients are enrolled !");
            }
        }

        /// <summary>
        /// View Patient by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="CustomHttpException"></exception>

        [HttpGet("{Id}")]
        public async Task<ActionResult<Patient>> ViewPatient(Guid Id) 
        {
            var patient = await _patient.GetPatientByID(Id);
            if (patient != null)
            {
                return Ok(patient);

            }
            throw new CustomHttpException("patient is not found !!", StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Adding Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        /// <exception cref="CustomHttpException"></exception>


        [HttpPost]
        public async Task<ActionResult<Patient>> AddPatients(Patient patient)
        {
            var addpatient = await _patient.AddPatient(patient);
            if(addpatient != null)
            {
                return Ok(StatusCodes.Status201Created);
            }

            throw new CustomHttpException("patient is not created !!", StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Edit the status of the patient
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        /// <exception cref="CustomHttpException"></exception>

        [HttpPut("{Id}")]
        public async Task<ActionResult<Patient>> EditPatient(Guid Id, Patient patient)
        {
            var editpatient = await _patient.EditPatient(Id, patient);
            if(editpatient != null)
            {
                return Ok(StatusCodes.Status200OK);
            }
            throw new CustomHttpException("patient is not Found!!", StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Delete patient who got discharged
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="CustomHttpException"></exception>

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Patient>> DeletePatient(Guid Id)
        {
            var deletepatient = await _patient.DeletePatient(Id);
            if (deletepatient != null)
            {
                return Ok(StatusCodes.Status200OK);
            }
            throw new CustomHttpException("patient is not deleted !!", StatusCodes.Status400BadRequest);
        }
    }
}
