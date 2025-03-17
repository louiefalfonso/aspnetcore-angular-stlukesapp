namespace StLukesMedicalApp.API.Models.Domain
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Specialization { get; set; }
        public string Department { get; set; }
        public string Schedule { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Prescription> Perscriptions { get; set; }
        public ICollection<Admission> Admissions { get; set; }
    }
}

