namespace StLukesMedicalApp.API.Models.Domain
{
    public class Billing
    {
        public Guid Id { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime DateOfBilling { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
