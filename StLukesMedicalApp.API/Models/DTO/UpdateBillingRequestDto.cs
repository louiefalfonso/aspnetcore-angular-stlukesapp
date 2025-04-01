namespace StLukesMedicalApp.API.Models.DTO
{
    public class UpdateBillingRequestDto
    {
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime DateOfBilling { get; set; }
        public string PaymentMethod { get; set; }
        public string Remarks { get; set; }
        public List<Guid> Patients { get; set; } = new List<Guid>();
    }
}
