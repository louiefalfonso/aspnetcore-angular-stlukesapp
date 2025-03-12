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
    }
}


