﻿using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;

        // add constructor
        public DoctorController(IDoctorRepository doctorRepository) 
        {
            this.doctorRepository = doctorRepository;
        }

        // Add New Doctor
        [HttpPost]
        public async Task<IActionResult> CreateNewDoctor([FromBody] CreateDoctorRequestDto request)
        {
            // Map DTO to Domain Model
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                ContactNumber = request.ContactNumber,
                Specialization = request.Specialization,
                Department = request.Department,
                Schedule = request.Schedule,
            };

            // Create New Doctor
            await doctorRepository.CreateAsync(doctor);

            // Map Domain Model to DTO
            var response = new DoctorDto
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                ContactNumber = doctor.ContactNumber,
                Specialization = doctor.Specialization,
                Department = doctor.Department,
                Schedule = doctor.Schedule,
            };

            return Ok(response);
        }


        // Get All Doctors
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            // Get All Doctors
            var doctors = await doctorRepository.GetAllAsync();

            // Map Domain Model to DTO
            var response = new List<DoctorDto>();
            foreach (var doctor in doctors)
            {
                response.Add(new DoctorDto {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Email = doctor.Email,
                    ContactNumber = doctor.ContactNumber,
                    Specialization = doctor.Specialization,
                    Department = doctor.Department,
                    Schedule = doctor.Schedule,
                });
            };
            return Ok(response);
        }


        // Get Doctor By ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetDoctorById([FromRoute]Guid id)
        {
            // Get Doctor By ID
            var doctor = await doctorRepository.GetByIdAsync(id);

            // Check if Doctor is Null
            if(doctor == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var response = new DoctorDto
            {
                Id = id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                ContactNumber = doctor.ContactNumber,
                Specialization = doctor.Specialization,
                Department = doctor.Department,
                Schedule = doctor.Schedule,
            };
            return Ok(response);
        }


        // Get Doctor By Department
        [HttpGet]
        [Route("{department}")]
        public async Task<IActionResult> GetDoctorByDepartment([FromRoute] string department)
        {
            // Get Doctor Details From Repository
            var doctor = await doctorRepository.GetByDepartmentAsync(department);

            // Check if Doctor is Null
            if (doctor is null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var response = new DoctorDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                ContactNumber = doctor.ContactNumber,
                Specialization = doctor.Specialization,
                Department = doctor.Department,
                Schedule = doctor.Schedule,
            };
            return Ok(response);
        }
    }
}
