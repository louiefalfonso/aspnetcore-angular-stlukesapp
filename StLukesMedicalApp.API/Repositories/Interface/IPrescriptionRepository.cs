using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IPrescriptionRepository
    {
        Task<Prescription> CreateAsync(Prescription prescription);

        Task<Prescription?> GetByIdAsync(Guid id);

        Task<IEnumerable<Prescription>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            );

        Task<Prescription?> UpdateAsync(Prescription prescription);

        Task<Prescription?> DeleteAsync(Guid id);
        Task<int> GetCount();
    }
}
