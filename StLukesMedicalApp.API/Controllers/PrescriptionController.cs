using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepository prescriptionRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;

        // add constructor
        public PrescriptionsController(
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

        // Get All Prescriptions
        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            // get all prescriptions from repository
            var prescriptions = await prescriptionRepository.GetAllAsync();

            // map domain model to DTO
            var response = new List<PrescriptionDto>();
            {
                foreach (var prescription in prescriptions)
                {
                    response.Add(new PrescriptionDto
                    {
                        Id= prescription.Id,
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
                    });
                } 
            }
            return Ok(response);
        }

        // Get Prescription by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPrescriptionById([FromRoute] Guid id)
        {
            // get prescription by ID
            var prescription = await prescriptionRepository.GetByIdAsync(id);

            // check if prescription is null
            if (prescription == null)
            {
                return NotFound();
            }

            // map domain model to DTO
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

        // Update Prescription
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePrescription([FromRoute] Guid id, UpdatePrescriptionRequestDto request)
        {
            // convert DTO to Domain Model
            var prescription = new Prescription
            {
                Id = id,
                MedicationList = request.MedicationList,
                Dosage = request.Dosage,
                DateIssued = request.DateIssued,
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>()
            };

            // Add Doctor to Prescription
            foreach (var doctorGuid in request.Doctors)
            {
                // Get Doctor By ID from Doctor Repository
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // Check if Doctor Exists
                if (exisitingDoctor is not null)
                {
                    prescription.Doctors.Add(exisitingDoctor);
                }
            }

            // Add Patient to Prescription
            foreach (var patientGuid in request.Patients)
            {
                // Get Patient By ID from Patient Repository
                var existingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // Check if Patient Exist
                if (existingPatient is not null)
                {
                    prescription.Patients.Add(existingPatient);
                }
            }

            // Call Repository to update Prescription Domain Model
            var updatedPrescription = await prescriptionRepository.UpdateAsync(prescription);

            // Check if Updated Prescription is Null
            if (updatedPrescription == null)
            {
                return NotFound();
            }

            // Convert domain model to DTO
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

        // Delete Prescription
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePrescription([FromRoute] Guid id) 
        { 
            // delete prescription
            var deletedPrescription = await prescriptionRepository.DeleteAsync(id);

            // check if deleted prescription is null
            if (deletedPrescription == null)
            {
                return NotFound();
            }

            // convert Domain Model to DTO
            var response = new PrescriptionDto
            {
                Id = deletedPrescription.Id,
                MedicationList = deletedPrescription.MedicationList,
                Dosage = deletedPrescription.Dosage,
                DateIssued = deletedPrescription.DateIssued,
            };
            return Ok(response);
        }
    }
}