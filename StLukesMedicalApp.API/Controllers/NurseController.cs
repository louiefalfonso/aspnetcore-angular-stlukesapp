﻿using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Implementation;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly INurseRepository nurseRepository;

        // Create Constructor
        public NursesController(INurseRepository nurseRepository )
        {
            this.nurseRepository = nurseRepository;
        }

        // Add New Nurse
        [HttpPost]
        public async Task<IActionResult> CreateNewNurse([FromBody] CreateNurseRequestDto request)
        {
            // Map DTO to Domain Model
            var nurse = new Nurse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                BadgeNumber = request.BadgeNumber,
                Qualifications = request.Qualifications,
                Schedule = request.Schedule,
                Department = request.Department,
            };

            // Add New Admission
           await nurseRepository.CreateAsync(nurse);

            // Map Domain Model to DTO
            var response = new NurseDto
            {
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                PhoneNumber = nurse.PhoneNumber,
                EmailAddress = nurse.EmailAddress,
                BadgeNumber = nurse.BadgeNumber,
                Qualifications = nurse.Qualifications,
                Schedule = nurse.Schedule,
                Department = nurse.Department,
               
            };
            return Ok(response);
        }

        // Get Nurse By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetNurseById([FromRoute] Guid id)
        {
            // Get Nurse By Id
            var nurse = await nurseRepository.GetByIdAsync(id);

            // Check if Nurse is Null
            if (nurse == null) {
                return NotFound();
            }

            // Map Domain Model to DTO
            var response = new NurseDto
            {
                Id = id,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                PhoneNumber = nurse.PhoneNumber,
                EmailAddress = nurse.EmailAddress,
                BadgeNumber = nurse.BadgeNumber,
                Qualifications = nurse.Qualifications,
                Schedule = nurse.Schedule,
                Department = nurse.Department,
               
            };

            return Ok(response);
        }

        // Get All Nurses
        [HttpGet]
        public async Task<IActionResult> GetAllNurses
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            // Get All Nurses from Repository
            var nurses = await nurseRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // Map Domaihn Model to DTO
            var response = new List<NurseDto>();
            foreach (var nurse in nurses)
            {
                response.Add(new NurseDto{
                    Id = nurse.Id,
                    FirstName = nurse.FirstName,
                    LastName = nurse.LastName,
                    PhoneNumber = nurse.PhoneNumber,
                    EmailAddress = nurse.EmailAddress,
                    BadgeNumber = nurse.BadgeNumber,
                    Qualifications = nurse.Qualifications,
                    Schedule = nurse.Schedule,
                    Department = nurse.Department,
                });
            };

            return Ok(response);
        }

        // Update Nurse
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateNurse([FromRoute] Guid id, UpdateNurseRequestDto request)
        {
            // Convert DTO to Domain
            var nurse = new Nurse
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                BadgeNumber = request.BadgeNumber,
                Qualifications = request.Qualifications,
                Schedule = request.Schedule,
                Department = request.Department,
            };

            // Update Nurse
            nurse = await nurseRepository.UpdateAsync(nurse);

            // Check if nurse is null
            if(nurse == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var response = new NurseDto
            {
                Id = nurse.Id,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                PhoneNumber = nurse.PhoneNumber,
                EmailAddress = nurse.EmailAddress,
                BadgeNumber = nurse.BadgeNumber,
                Qualifications = nurse.Qualifications,
                Schedule = nurse.Schedule,
                Department = nurse.Department,
            };

            return Ok(response);
        }

        // Delete Nurse
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNurse([FromRoute] Guid id)
        {
            // Get Nurse By Id
            var nurse = await nurseRepository.DeleteAsync(id);

            // Check if Nurse is Null
            if (nurse == null) 
            { 
                return NotFound();
            }

            // Map Domain Model DTO
            var response = new NurseDto
            { 
                Id = nurse.Id,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                PhoneNumber = nurse.PhoneNumber,
                EmailAddress = nurse.EmailAddress,
                BadgeNumber = nurse.BadgeNumber,
                Qualifications = nurse.Qualifications,
                Schedule = nurse.Schedule,
                Department = nurse.Department,
            };
            return Ok(response);
        }

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetNursesTotal()
        {
            var count = await nurseRepository.GetCount();
            return Ok(count);
        }
    }
}
