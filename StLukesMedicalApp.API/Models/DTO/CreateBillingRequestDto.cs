namespace StLukesMedicalApp.API.Models.DTO
{
    public class CreateBillingRequestDto
    {
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime DateOfBilling { get; set; }
        public string PaymentMethod { get; set; }
        public Guid[] Patients { get; set; }
    }
}
