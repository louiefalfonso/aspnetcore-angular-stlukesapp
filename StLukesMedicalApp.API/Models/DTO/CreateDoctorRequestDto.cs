namespace StLukesMedicalApp.API.Models.DTO
{
    public class CreateDoctorRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Specialization { get; set; }
        public string Department { get; set; }
        public string Schedule { get; set; }
    }
}
