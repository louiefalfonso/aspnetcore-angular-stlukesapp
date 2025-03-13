namespace StLukesMedicalApp.API.Models.Domain
{
    public class Nurse
    {
        public Guid Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string EmailAddress{ get; set; }
        public string PhoneNumber { get; set; }
        public string BadgeNumber { get; set; }
        public string Qualifications { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}
