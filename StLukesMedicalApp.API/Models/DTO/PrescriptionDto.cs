namespace StLukesMedicalApp.API.Models.DTO
{
    public class PrescriptionDto
    {
        public Guid Id { get; set; }
        public string MedicationList { get; set; }
        public string Dosage { get; set; }
        public DateTime DateIssued { get; set; }
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
    }
}
