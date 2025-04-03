using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAsync(Appointment appointment);

        Task<IEnumerable<Appointment>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            );

        Task<Appointment?> GetByIdAsync(Guid id);

        Task<Appointment?> UpdateAsync(Appointment appointment);

        Task<Appointment?> DeleteAsync(Guid id);
    }
}
