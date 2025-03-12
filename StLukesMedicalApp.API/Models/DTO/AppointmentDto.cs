using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Models.DTO
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Date { get; set; }
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
    }
}
