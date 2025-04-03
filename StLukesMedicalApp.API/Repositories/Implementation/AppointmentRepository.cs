using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        // Create Constructor
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

        // Get All Appointments
        public async Task<IEnumerable<Appointment>> GetAllAsync
            (
                // add filtering, sorting & pagination
                string? query = null,
                string? sortBy = null,
                string? sortDirection = null,
                int? pageNumber = 1,
                int? pageSize = 100
            )
        {
            // query
            var appointments = dbContext.Appointments.AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                appointments = appointments.Where(x =>
                    x.Status.Contains(query));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "status", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    appointments = isAsc ? appointments.OrderBy(x => x.Status) : appointments.OrderByDescending(x => x.Status);
                }
            }


            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            appointments = appointments.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await appointments.Include(x => x.Doctors).Include(x => x.Patients).ToListAsync();
        }

        // Get Appointment By ID
        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
           return await dbContext.Appointments.Include(x => x.Doctors).Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == id);
        }

        // Update Appointment
        public async Task<Appointment?> UpdateAsync(Appointment appointment)
        {
            // Fetch The Appointment By ID
            var exisitingAppointment = await dbContext.Appointments.Include(x=>x.Doctors).Include(x=> x.Patients).FirstOrDefaultAsync(x => x.Id == appointment.Id);

            // Check if existing appointment is null
            if (exisitingAppointment == null) 
            {
                return null;
            }

            // Update Appointment
            dbContext.Entry(exisitingAppointment).CurrentValues.SetValues(appointment);

            // Update Doctor
            exisitingAppointment.Doctors = appointment.Doctors;

            // Update Patient
            exisitingAppointment.Patients = appointment.Patients;

            // Save Changes
            await dbContext.SaveChangesAsync();

            return exisitingAppointment;
        }

        // Delete Appointment
        public async Task<Appointment?> DeleteAsync(Guid id)
        {
            // Fetch The Appointment By ID
            var existingAppointment = await dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            // Check if Existing Appointment is Null
            if (existingAppointment != null) 
            { 
                dbContext.Appointments.Remove(existingAppointment);
                await dbContext.SaveChangesAsync();
                return existingAppointment;
            }

            return null;
        }
    }
}
