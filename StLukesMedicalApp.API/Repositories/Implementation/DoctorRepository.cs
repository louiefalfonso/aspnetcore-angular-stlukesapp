using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DoctorRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Add New Doctor
        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            await dbContext.Doctors.AddAsync(doctor);
            await dbContext.SaveChangesAsync();
            return doctor;
        }

        // Get All Doctors
        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await dbContext.Doctors.ToListAsync();
        }

        // Get Doctor By ID
        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            return await dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Update Doctor
        public async Task<Doctor?> UpdateAsync(Doctor doctor)
        {
            var existingDoctor = await dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == doctor.Id);

            if (existingDoctor != null)
            {
                dbContext.Entry(existingDoctor).CurrentValues.SetValues(doctor);
                await dbContext.SaveChangesAsync();
                return doctor;
            }

            return null;
        }

        // Delete Doctor
        public async Task<Doctor?> DeleteAsync(Guid id)
        {
            // Get existing Doctor By ID
            var existingDoctor = await dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);

            // Check if Exixsting Doctor is null
            if (existingDoctor is null)
            {
                return null;
            }

            // Delete Doctor & Save Changes
            dbContext.Doctors.Remove(existingDoctor);
            await dbContext.SaveChangesAsync();
            return existingDoctor;
        }

        // Get All Doctors By Department
        public async Task<IEnumerable<Doctor>> GetAllDoctorsByDepartment(string department)
        {
            return await dbContext.Doctors.Where(b => b.Department.ToLower() == department.ToLower()).ToListAsync();
        }

        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Doctors.CountAsync();
        }
    }
}


