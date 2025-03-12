using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        // Create COnstructor
        public AppointmentRepository (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Add New Appointment
        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
           await dbContext.Appointments.AddAsync(appointment);
           await dbContext.SaveChangesAsync();
           return appointment;
        }
    }
}
