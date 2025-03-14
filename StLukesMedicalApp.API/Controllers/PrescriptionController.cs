using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepository prescriptionRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;

        // add constructor
        public PrescriptionController(
            IPrescriptionRepository prescriptionRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository
            )
        {
            this.prescriptionRepository = prescriptionRepository;
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }

        // Add New Prescription
        [HttpPost]
        public async Task<IActionResult> CreateNewPrescription([FromBody] CreatePrescriptionRequestDto request)
        {
            // Map DTO to Domain Model
            var prescription = new Prescription
            {
                MedicationList = request.MedicationList,
                Dosage = request.Dosage,
                DateIssued = request.DateIssued,
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
            };


            // Add Doctor to Appointment
            foreach (var doctorGuid in request.Doctors)
            {
                // get doctor by Id
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // check if doctor is null
                if (exisitingDoctor is not null)
                {
                    prescription.Doctors.Add(exisitingDoctor);
                }

            }

            // Add Patient to Appointment
            foreach (var patientGuid in request.Patients)
            {
                // get patient by Id
                var exisitingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // check if patient is nulll
                if (exisitingPatient is not null)
                {
                    prescription.Patients.Add(exisitingPatient);
                }
            }

            // Add New Prescription
            prescription = await prescriptionRepository.CreateAsync(prescription);

            // Map Domain Model to DTO
            var response = new PrescriptionDto
            {
                Id = prescription.Id,
                MedicationList = prescription.MedicationList,
                Dosage = prescription.Dosage,
                DateIssued = prescription.DateIssued,
                Doctors = prescription.Doctors.Select(x => new DoctorDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Specialization = x.Specialization,
                    Department = x.Department,
                    Schedule = x.Schedule,
                }).ToList(),

                Patients = prescription.Patients.Select(x => new PatientDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Sex = x.Sex,
                    Age = x.Age,
                    Address = x.Address,
                    Diagnosis = x.Diagnosis,
                    PatientType = x.PatientType,
                }).ToList()

            };
            return Ok(response);
        }
    }
}
