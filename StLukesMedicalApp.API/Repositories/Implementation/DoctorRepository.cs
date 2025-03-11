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

        // Get Doctor By Department
        public async Task<Doctor?> GetByDepartmentAsync(string department)
        {
            return await dbContext.Doctors.FirstOrDefaultAsync( x => x.Department == department);
        }


        // Update Doctor
        public  async Task<Doctor?> UpdateAsync(Doctor doctor)
        {
           // Get Doctor By ID
           var existingDoctor = dbContext.Doctors.FirstOrDefaultAsync( x => x.Id == doctor.Id);

            // Check if Doctor is not Null
            if (existingDoctor != null) 
            { 
                dbContext.Entry(existingDoctor).CurrentValues.SetValues(doctor);
            }
            return null;
        }
    }
}


