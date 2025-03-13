using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAsync(Appointment appointment);

        Task<IEnumerable<Appointment>> GetAllAsync();

        Task<Appointment?> GetByIdAsync(Guid id);

        Task<Appointment?> UpdateAsync(Appointment appointment);

        Task<Appointment?> DeleteAsync(Guid id);
    }
}
