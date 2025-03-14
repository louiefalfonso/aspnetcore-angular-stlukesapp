using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IPrescriptionRepository
    {
        Task<Prescription> CreateAsync(Prescription prescription);

        Task<Prescription?> GetByIdAsync(Guid id);

        Task<IEnumerable<Prescription>> GetAllAsync();

        Task<Prescription?> UpdateAsync(Prescription prescription);

        Task<Prescription?> DeleteAsync(Guid id);
    }
}
