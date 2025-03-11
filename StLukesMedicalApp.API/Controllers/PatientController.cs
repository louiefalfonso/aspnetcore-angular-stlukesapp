using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientController: ControllerBase
    {
        private readonly IPatientRepository patientRepository;

        public PatientController(IPatientRepository patientRepository) 
        {
            this.patientRepository = patientRepository;
        }

        // Add New Patient
        [HttpPost]
        public async Task<IActionResult> CreateNewPatient([FromBody] CreatePatientRequestDto request)
        {
            // map dto to domain model
            var patient = new Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                ContactNumber = request.ContactNumber,
                Sex = request.Sex,
                Age = request.Age,
                Address = request.Address,
                Diagnosis = request.Diagnosis,
                PatientType = request.PatientType,

            };

            await patientRepository.CreateAsync(patient);

            // map domail model to dto
            var response = new PatientDto
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                ContactNumber = patient.ContactNumber,
                Sex = patient.Sex,
                Age = patient.Age,
                Address = patient.Address,
                Diagnosis = patient.Diagnosis,
                PatientType = patient.PatientType,
            };

            return Ok(response);
        }
    }
}
