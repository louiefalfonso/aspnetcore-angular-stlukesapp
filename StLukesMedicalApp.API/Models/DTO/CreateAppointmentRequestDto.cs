using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Models.DTO
{
    public class CreateAppointmentRequestDto
    {
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public Guid[] Doctors { get; set; }
        public Guid[] Patients { get; set; }
    }

}
