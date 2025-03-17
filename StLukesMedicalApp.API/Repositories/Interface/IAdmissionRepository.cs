using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IAdmissionRepository
    {
        Task<Admission> CreateAsync(Admission admission);

        Task<IEnumerable<Admission>> GetAllAsync();

        Task<Admission?> GetByIdAsync (Guid id);

        Task<Admission?> UpdateAsync(Admission admission);
    }
}
