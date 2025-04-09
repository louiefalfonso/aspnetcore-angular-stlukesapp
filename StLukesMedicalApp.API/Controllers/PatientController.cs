using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientsController: ControllerBase
    {
        private readonly IPatientRepository patientRepository;

        public PatientsController(IPatientRepository patientRepository) 
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


        // Get All Patients
        [HttpGet]
        public async Task<IActionResult> GetAllPatients
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            var patients = await patientRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // map Domain model to DTO
            var response = new List<PatientDto>();
            foreach (var patient in patients)
            {
                response.Add(new PatientDto
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    ContactNumber = patient.ContactNumber,
                    Sex = patient.Sex,
                    Age = patient.Age,
                    Address = patient.Address,
                    Diagnosis = patient.Diagnosis,
                    PatientType = patient.PatientType,
                });
            }
            return Ok(response);
        }

        // Get Patient By ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPatientById([FromRoute] Guid id)
        {
            // get patient by Id
            var patient = await patientRepository.GetByIdAsync(id);

            // check if patient is null
            if (patient == null)
            {
                return NotFound();
            }

            // map domain model to DTO
            var response = new PatientDto
            {
                Id = patient.Id,
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

        // Update Patient
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] Guid id, UpdatePatientRequestDto request)
        {
            // convert dto to domain model
            var patient = new Patient
            {
                Id = id,
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

            // update patient
            patient = await patientRepository.UpdateAsync(patient);

            // check if patient is null
            if (patient == null) 
            {
                return NotFound();
            }

            // map domain model to Dto
            var response = new PatientDto
            {
                Id = patient.Id,
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

        // Delete Patient
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePatient([FromRoute] Guid id)
        {
            // delete patient
            var patient = await patientRepository.DeleteAsync(id);

            //check if patient is null
            if (patient == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new PatientDto
            { 
                Id = patient.Id,
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

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetPatientsTotal()
        {
            var count = await patientRepository.GetCount();
            return Ok(count);
        }

        [HttpPost("import")]
        public async Task<IActionResult> CreatePatientsInBulk([FromBody] IEnumerable<Patient> patients)
        {
            if (patients == null || !patients.Any())
            {
                return BadRequest("Patients collection cannot be null or empty.");
            }

            var createdPatients = await patientRepository.CreateInBulkAsync(patients);
            return Ok(createdPatients);
        }
    }
}
