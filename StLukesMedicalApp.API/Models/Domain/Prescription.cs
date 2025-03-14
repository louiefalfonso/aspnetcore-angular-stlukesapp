namespace StLukesMedicalApp.API.Models.Domain
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public string MedicationList { get; set; }
        public string Dosage { get; set; }
        public DateTime DateIssued { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
