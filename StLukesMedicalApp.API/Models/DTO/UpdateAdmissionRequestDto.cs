using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Models.DTO
{
    public class UpdateAdmissionRequestDto
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string AvailabilityStatus { get; set; }
        public DateTime Date { get; set; }
        public List<Guid> Doctors { get; set; } = new List<Guid>();
        public List<Guid> Patients { get; set; } = new List<Guid>();
        public List<Guid> Nurses { get; set; } = new List<Guid>();
    }
}
