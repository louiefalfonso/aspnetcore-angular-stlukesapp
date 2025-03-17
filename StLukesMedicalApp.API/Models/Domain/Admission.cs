namespace StLukesMedicalApp.API.Models.Domain
{
    public class Admission
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string AvailabilityStatus { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Nurse> Nurses { get; set; }
    }
}
