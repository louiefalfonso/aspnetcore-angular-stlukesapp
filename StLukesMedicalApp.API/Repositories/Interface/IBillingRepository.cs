using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IBillingRepository
    {
        Task<Billing> CreateAsync(Billing billing);

        Task<Billing?> GetByIdAsync(Guid id);

        Task<IEnumerable<Billing>> GetAllAsync();

        Task<Billing?> UpdateAsync(Billing billing);

        Task<Billing?> DeleteAsync(Guid id);

        Task<IEnumerable<Billing>> GetAllBillingByPaymentStatusAsync(string paymentStatus);
    }
}
