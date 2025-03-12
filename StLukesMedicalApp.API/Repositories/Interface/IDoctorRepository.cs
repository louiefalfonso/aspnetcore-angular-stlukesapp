using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateAsync(Doctor doctor);

        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<Doctor?> GetByIdAsync(Guid id);

        Task<Doctor?> GetByDepartmentAsync(string department);

        Task<Doctor?> UpdateAsync(Doctor doctor);

        Task<Doctor?> DeleteAsync(Guid id);
    }
}
