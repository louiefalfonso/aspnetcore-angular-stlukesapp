namespace StLukesMedicalApp.API.Models.DTO
{
    public class CreatePrescriptionRequestDto
    {
        public string MedicationList { get; set; }
        public string Dosage { get; set; }
        public DateTime DateIssued { get; set; }
        public string Remarks { get; set; }
        public Guid[] Doctors { get; set; }
        public Guid[] Patients { get; set; }
    }
}
