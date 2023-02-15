using AutoMapper;
using HospitalManagement.Domain.Entities;
using HospitalManagement.Domain.Repositories;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Api.Mappings
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Patient,PatientEntity>();
            CreateMap<Doctor,DoctorEntity>();
            CreateMap<Appointment, AppointmentEntity>();

        }
    }
}
