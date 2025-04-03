using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IAdmissionRepository
    {
        Task<Admission> CreateAsync(Admission admission);

        Task<IEnumerable<Admission>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            );

        Task<Admission?> GetByIdAsync (Guid id);

        Task<Admission?> UpdateAsync(Admission admission);

        Task<Admission?> DeleteAsync(Guid id);

        Task<int> GetCount();
    }
}
