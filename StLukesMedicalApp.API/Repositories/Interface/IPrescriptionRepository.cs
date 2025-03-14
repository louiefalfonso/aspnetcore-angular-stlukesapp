using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface IPrescriptionRepository
    {
        Task<Prescription> CreateAsync(Prescription prescription);

        Task<Prescription?> GetByIdAsync(Guid id);

        /*
      
        Task<Appointment?> GetByIdAsync(Guid id);

        Task<Appointment?> UpdateAsync(Appointment appointment);

        Task<Appointment?> DeleteAsync(Guid id);
         */
    }
}
