using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IBillingRepository
    {
        Task<Billing> CreateAsync(Billing billing);

        Task<Billing?> GetByIdAsync(Guid id);

        Task<IEnumerable<Billing>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            );

        Task<Billing?> UpdateAsync(Billing billing);

        Task<Billing?> DeleteAsync(Guid id);

        Task<IEnumerable<Billing>> GetAllBillingByPaymentStatusAsync(string paymentStatus);

        Task<int> GetCount();
    }
}
