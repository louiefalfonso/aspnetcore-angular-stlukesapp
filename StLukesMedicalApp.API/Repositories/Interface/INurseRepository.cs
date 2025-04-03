using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface INurseRepository
    {
        Task<Nurse> CreateAsync(Nurse nurse);

        Task<Nurse?> GetByIdAsync(Guid id);

        Task<IEnumerable<Nurse>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            );

        Task<Nurse?> UpdateAsync(Nurse nurse);

        Task<Nurse?> DeleteAsync(Guid id);

        Task<int> GetCount();
    }
}
