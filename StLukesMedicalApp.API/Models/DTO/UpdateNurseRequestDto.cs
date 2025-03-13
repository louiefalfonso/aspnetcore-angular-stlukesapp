namespace StLukesMedicalApp.API.Models.DTO
{
    public class UpdateNurseRequestDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BadgeNumber { get; set; }
        public string Qualifications { get; set; }
    }
}
