using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IDoctorRepository doctorRepository;

        // add constructor
        public AppointmentController(
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository
            )
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
        }

        // Add New Appointment
        [HttpPost]
        public async Task<IActionResult> CreateNewAppointment([FromBody] CreateAppointmentRequestDto request)
        {
            // Map DTO to Domain Model
            var appointment = new Appointment
            {
                Status = request.Status,
                Comments = request.Comments,
                Diagnosis = request.Diagnosis,
                Date = request.Date,
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
                    appointment.Doctors.Add(exisitingDoctor);
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
                    appointment.Patients.Add(exisitingPatient);
                }
            }

            // Add New Appointment
            appointment = await appointmentRepository.CreateAsync(appointment);

            // Map Domain to DTO
            var response = new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Comments = appointment.Comments,
                Diagnosis = appointment.Diagnosis,
                Date = appointment.Date,
                Doctors = appointment.Doctors.Select(x => new DoctorDto
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

                Patients = appointment.Patients.Select(x => new PatientDto
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


        // Get All Appointments
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            // Get All Appointments
            var appointments = await appointmentRepository.GetAllAsync();

            // Map Domain Model to DTO
            var response = new List<AppointmentDto>();
            {
                foreach (var appointment in appointments)
                {
                    response.Add(new AppointmentDto
                    {
                        Id = appointment.Id,
                        Status = appointment.Status,
                        Comments = appointment.Comments,
                        Diagnosis = appointment.Diagnosis,
                        Date = appointment.Date,
                        Doctors = appointment.Doctors.Select(x => new DoctorDto
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
                        Patients = appointment.Patients.Select(x => new PatientDto
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


        // Get Appointment By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAppointmentById([FromRoute] Guid id)
        {
            // Get Appointment By ID
            var appointment = await appointmentRepository.GetByIdAsync(id);

            // Check if Appointment is null
            if (appointment == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var response = new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Comments = appointment.Comments,
                Diagnosis = appointment.Diagnosis,
                Date = appointment.Date,
                Doctors = appointment.Doctors.Select(x => new DoctorDto
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
                Patients = appointment.Patients.Select(x => new PatientDto
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


        // Update Appointment
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, UpdateAppointmentRequestDto request)
        {
            // Convert DTO to Domain Model
            var appointment = new Appointment
            {
                Id = id,
                Status = request.Status,
                Comments = request.Comments,
                Diagnosis = request.Diagnosis,
                Date = request.Date,
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>()
            };

            // Add Doctor to Appointment
            foreach (var doctorGuid in request.Doctors)
            {
                // Get Doctor By ID from Doctor Repository
                var exisitingDoctor = await doctorRepository.GetByIdAsync(doctorGuid);

                // Check if Doctor Exists
                if (exisitingDoctor is not null) 
                {
                    appointment.Doctors.Add(exisitingDoctor);
                }
            }

            // Add Patient to Appointment
            foreach (var patientGuid in request.Patients)
            {
                // Get Patient By ID from Patient Repository
                var existingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // Check if Patient Exist
                if (existingPatient is not null) 
                { 
                    appointment.Patients.Add(existingPatient);
                }
            }

            // Call Repository to update Appointment Domain Model
            var updatedAppointment = await appointmentRepository.UpdateAsync(appointment);

            // Check if Updated Appointment is Null
            if (updatedAppointment == null) 
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var response = new AppointmentDto
            {
                Id = appointment.Id,
                Status = appointment.Status,
                Comments = appointment.Comments,
                Diagnosis = appointment.Diagnosis,
                Date = appointment.Date,
                Doctors = appointment.Doctors.Select(x => new DoctorDto
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
                Patients = appointment.Patients.Select(x => new PatientDto
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

        // Delete Appointment
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAppointment([FromRoute] Guid id)
        {
            // Delete Appointment
            var deletedAppointment = await appointmentRepository.DeleteAsync(id);

            // Check if Deleted Appointment is Null
            if (deletedAppointment == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var response = new AppointmentDto
            {
                Id = deletedAppointment.Id,
                Status = deletedAppointment.Status,
                Comments = deletedAppointment.Comments,
                Diagnosis = deletedAppointment.Diagnosis,
                Date = deletedAppointment.Date,

            };
            return Ok(response);
        }

    }
}
