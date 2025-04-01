namespace StLukesMedicalApp.API.Models.DTO
{
    public class UpdatePrescriptionRequestDto
    {
        public string MedicationList { get; set; }
        public string Dosage { get; set; }
        public DateTime DateIssued { get; set; }
        public string Remarks { get; set; }
        public List<Guid> Doctors { get; set; } = new List<Guid>();
        public List<Guid> Patients { get; set; } = new List<Guid>();
    }
}
