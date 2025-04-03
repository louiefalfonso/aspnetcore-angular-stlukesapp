using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateAsync(Doctor doctor);

        Task<IEnumerable<Doctor>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            );

        Task<Doctor?> GetByIdAsync(Guid id);

        Task<Doctor?> UpdateAsync(Doctor doctor);

        Task<Doctor?> DeleteAsync(Guid id);

        Task<IEnumerable<Doctor>> GetAllDoctorsByDepartment(string department);

        Task<int> GetCount();
    }
}
