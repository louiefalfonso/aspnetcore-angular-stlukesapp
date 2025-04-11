using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Implementation;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdmissionsController : ControllerBase
    {
        private readonly IAdmissionRepository admissionRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly INurseRepository nurseRepository;
        private readonly IPatientRepository patientRepository;

        // add constructor
        public AdmissionsController(
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
                Remarks = request.Remarks,
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
                Remarks = admission.Remarks,
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
                    Department =x.Department,
                    Schedule = x.Schedule,

                }).ToList()

            };

            return Ok(response);
        }


        // get all admissions
        [HttpGet]
        public async Task<IActionResult> GetAllAdmissions
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            // get all admissions
            var admissions = await admissionRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

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
                        Remarks = admission.Remarks,
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
                            Department = x.Department,
                            Schedule = x.Schedule,

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
                Remarks = admission.Remarks,
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
                    Department = x.Department,
                    Schedule = x.Schedule,

                }).ToList()
            };

            return Ok(response);

        }

        // update admission
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAdmission([FromRoute] Guid id, UpdateAdmissionRequestDto request)
        {
            // convert DTO to domain model
            var admission = new Admission
            {
                Id = id,
                RoomNumber = request.RoomNumber,
                RoomType = request.RoomType,
                AvailabilityStatus = request.AvailabilityStatus,
                Date = request.Date,
                Remarks = request.Remarks,
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>(),
            };


            // add doctor to this admission
            foreach (var doctorGuid in request.Doctors)
            {
                // get doctor by ID from doctor repository
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // Check if Doctor Exists
                if (exisitingDoctor is not null)
                {
                    admission.Doctors.Add(exisitingDoctor);
                }
            }

            // add nurse to this admission
            foreach (var nurseGuid in request.Nurses)
            {
                // get nurse by ID from doctor repository
                var exisitingNurse = await nurseRepository.GetByIdAsync(nurseGuid);

                // Check if Doctor Exists
                if (exisitingNurse is not null)
                {
                    admission.Nurses.Add(exisitingNurse);
                }
            }

            // add patient to this admission
            foreach (var patientGuid in request.Patients)
            {
                // get patient by ID from doctor repository
                var exisitingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // Check if Doctor Exists
                if (exisitingPatient is not null)
                {
                    admission.Patients.Add(exisitingPatient);
                }
            }

            // call repository to update admission Domain Model
            var updatedAdmission = await admissionRepository.UpdateAsync(admission);

            // check if updated admission is Null
            if (updatedAdmission == null)
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
                Remarks = admission.Remarks,
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
                    Department = x.Department,
                    Schedule = x.Schedule

                }).ToList()
            };

            return Ok(response);
        }

        // delete admission
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAdmission([FromRoute] Guid id)
        {
            // delete admission
            var deletedAdmission = await admissionRepository.DeleteAsync(id);

            // check if deleted admission is null
            if (deletedAdmission == null) 
            {
                return NotFound();
            }

            // convert domain model to DTO
            var response = new AdmissionDto
            {
                Id = deletedAdmission.Id,
                RoomNumber = deletedAdmission.RoomNumber,
                RoomType = deletedAdmission.RoomType,
                AvailabilityStatus = deletedAdmission.AvailabilityStatus,
                Date = deletedAdmission.Date,
                Remarks = deletedAdmission.Remarks,
            };

            return Ok(response);
        }

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetAdmissionsTotal()
        {
            var count = await admissionRepository.GetCount();
            return Ok(count);
        }

     
    }
}
