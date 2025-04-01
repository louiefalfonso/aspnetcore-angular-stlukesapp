namespace StLukesMedicalApp.API.Models.DTO
{
    public class BillingDto
    {
        public Guid Id { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime DateOfBilling { get; set; }
        public string PaymentMethod { get; set; }
        public string Remarks { get; set; }
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
    }
}
