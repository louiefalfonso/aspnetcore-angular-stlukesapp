using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface INurseRepository
    {
        Task<Nurse> CreateAsync(Nurse nurse);

        Task<Nurse?> GetByIdAsync(Guid id);

        Task<IEnumerable<Nurse>> GetAllAsync();

        Task<Nurse?> UpdateAsync(Nurse nurse);

        Task<Nurse?> DeleteAsync(Guid id);
    }
}
