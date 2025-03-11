namespace StLukesMedicalApp.API.Models.DTO
{
    public class UpdatePatientRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Diagnosis { get; set; }
        public string PatientType { get; set; }
    }
}
