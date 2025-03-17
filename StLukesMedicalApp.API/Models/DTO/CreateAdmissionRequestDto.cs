using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Models.DTO
{
    public class CreateAdmissionRequestDto
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string AvailabilityStatus { get; set; }
        public DateTime Date { get; set; }
        public Guid[] Doctors { get; set; }
        public Guid[] Patients { get; set; }
        public Guid[] Nurses { get; set; }
    }
}
