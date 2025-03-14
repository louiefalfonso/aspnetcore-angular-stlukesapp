namespace StLukesMedicalApp.API.Models.Domain
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Diagnosis { get; set; }
        public string PatientType { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
 
    }
}