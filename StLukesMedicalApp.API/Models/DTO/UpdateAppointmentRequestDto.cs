using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Models.DTO
{
    public class UpdateAppointmentRequestDto
    {
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Date { get; set; }
        public List<Guid> Doctors { get; set; } = new List<Guid>();
        public List<Guid> Patients { get; set; } = new List<Guid>();
    }
}
