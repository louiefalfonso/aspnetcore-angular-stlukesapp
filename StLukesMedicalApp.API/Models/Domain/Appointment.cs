namespace StLukesMedicalApp.API.Models.Domain
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Diagnosis { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}
