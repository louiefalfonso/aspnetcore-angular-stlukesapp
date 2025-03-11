using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IPatientRepository
    {
        Task<Patient> CreateAsync(Patient patient);

        Task<IEnumerable<Patient>> GetAllAsync();

        Task<Patient?> GetByIdAsync(Guid id);

        Task<Patient?> UpdateAsync(Patient patient);

        Task<Patient?> DeleteAsync(Guid id);
    }
}
