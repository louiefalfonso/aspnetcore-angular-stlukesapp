using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NurseController : ControllerBase
    {
        private readonly INurseRepository nurseRepository;

        // Create Constructor
        public NurseController(INurseRepository nurseRepository )
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
                Qualifications = request.Qualifications
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
               
            };

            return Ok(response);
        }

        // Get All Nurses
        [HttpGet]
        public async Task<IActionResult> GetAllNurses()
        {
            // Get All Nurses from Repository
            var nurses = await nurseRepository.GetAllAsync();

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
                });
            };

            return Ok(response);
        }
    }
}
