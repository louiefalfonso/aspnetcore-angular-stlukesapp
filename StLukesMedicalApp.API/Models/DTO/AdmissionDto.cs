namespace StLukesMedicalApp.API.Models.DTO
{
    public class AdmissionDto
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string AvailabilityStatus { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
        public List<NurseDto> Nurses { get; set; } = new List<NurseDto>();
    }
}
