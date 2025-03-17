using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionRepository admissionRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly INurseRepository nurseRepository;
        private readonly IPatientRepository patientRepository;

        // add constructor
        public AdmissionController(
            IAdmissionRepository admissionRepository,
            IDoctorRepository doctorRepository,
            INurseRepository nurseRepository,
            IPatientRepository patientRepository
            )
        {
            this.admissionRepository = admissionRepository;
            this.doctorRepository = doctorRepository;
            this.nurseRepository = nurseRepository;
            this.patientRepository = patientRepository;
        }

        // add new admission
        [HttpPost]
        public async Task<IActionResult> CreateNewAdmission([FromBody] CreateAdmissionRequestDto request)
        {
            // map domain model to DTO
            var admission = new Admission
            {
                RoomNumber = request.RoomNumber,
                RoomType = request.RoomType,
                AvailabilityStatus = request.AvailabilityStatus,
                Date = request.Date,
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>(),

            };

            // add doctor to this admission
            foreach (var doctorGuid in request.Doctors)
            {
                // get doctor by Id
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // check if doctor is null
                if (exisitingDoctor is not null)
                {
                    admission.Doctors.Add(exisitingDoctor);
                }

            }

            // add patient to this admission
            foreach (var patientGuid in request.Patients)
            {
                // get patient by Id
                var exisitingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // check if patient is nulll
                if (exisitingPatient is not null)
                {
                    admission.Patients.Add(exisitingPatient);
                }
            }

            // add nurse to this admission
            foreach (var nurseGuid in request.Nurses)
            {
                // get nurse by Id
                var exisitingNurse = await nurseRepository.GetByIdAsync(nurseGuid);

                // check if patient is nulll
                if (exisitingNurse is not null)
                {
                    admission.Nurses.Add(exisitingNurse);
                }
            }

            // add new admission
            admission = await admissionRepository.CreateAsync(admission);

            // map domain model to DTO
            var response = new AdmissionDto
            {
                Id = admission.Id,
                RoomNumber = admission.RoomNumber,
                RoomType = admission.RoomType,
                AvailabilityStatus = admission.AvailabilityStatus,
                Date = admission.Date,
                Doctors = admission.Doctors.Select(x => new DoctorDto
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

                Patients = admission.Patients.Select(x => new PatientDto
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
                }).ToList(),

                Nurses = admission.Nurses.Select(x => new NurseDto
                { 
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    BadgeNumber = x.BadgeNumber,
                    Qualifications = x.Qualifications,

                }).ToList()

            };

            return Ok(response);
        }

        // get all admissions
        [HttpGet]
        public async Task<IActionResult> GetAllAdmissions()
        {
            // get all admissions
            var admissions = await admissionRepository.GetAllAsync();

            // map domain model to DTO
            var response = new List<AdmissionDto>();
            {
                foreach (var admission in admissions)
                {
                    response.Add(new AdmissionDto 
                    {
                        Id = admission.Id,
                        RoomNumber = admission.RoomNumber,
                        RoomType = admission.RoomType,
                        AvailabilityStatus = admission.AvailabilityStatus,
                        Date = admission.Date,
                        Doctors = admission.Doctors.Select(x => new DoctorDto
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

                        Patients = admission.Patients.Select(x => new PatientDto
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
                        }).ToList(),

                        Nurses = admission.Nurses.Select(x => new NurseDto
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            EmailAddress = x.EmailAddress,
                            PhoneNumber = x.PhoneNumber,
                            BadgeNumber = x.BadgeNumber,
                            Qualifications = x.Qualifications,

                        }).ToList()
                    });
                }
            }

            return Ok(response);
        }

        // get admission by ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAdmissionById([FromRoute] Guid id)
        { 
            // get admission by ID
            var admission = await admissionRepository.GetByIdAsync(id);

            // Check if Appointment is null
            if (admission == null)
            {
                return NotFound();
            }

            // map domain model to DTO
            var response = new AdmissionDto
            {
                Id = admission.Id,
                RoomNumber = admission.RoomNumber,
                RoomType = admission.RoomType,
                AvailabilityStatus = admission.AvailabilityStatus,
                Date = admission.Date,
                Doctors = admission.Doctors.Select(x => new DoctorDto
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

                Patients = admission.Patients.Select(x => new PatientDto
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
                }).ToList(),

                Nurses = admission.Nurses.Select(x => new NurseDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    BadgeNumber = x.BadgeNumber,
                    Qualifications = x.Qualifications,

                }).ToList()
            };

            return Ok(response);

        }
    }
}