using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PatientRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        // Add New Patient
        public async Task<Patient> CreateAsync(Patient patient)
        {
           await dbContext.Patients.AddAsync(patient);
           await dbContext.SaveChangesAsync();
           return patient;
        }

        // Get All Patients
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await dbContext.Patients.ToListAsync();
        }

        // Get Patient By ID
        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Update Patient
        public async Task<Patient?> UpdateAsync(Patient patient)
        {
           var existingPatient = await dbContext.Patients.FirstOrDefaultAsync( x=>x.Id == patient.Id);

            if (existingPatient != null) 
            { 
                dbContext.Entry(existingPatient).CurrentValues.SetValues(patient);
                await dbContext.SaveChangesAsync();
                return patient;
            }

            return null;

        }

        // Delete Patient
        public async Task<Patient?> DeleteAsync(Guid id)
        {
           var existingPatient = dbContext.Patients.FirstOrDefault(x => x.Id == id);

            if (existingPatient is null) 
            {
                return null;
            }

            dbContext.Patients.Remove(existingPatient);
            await dbContext.SaveChangesAsync();
            return existingPatient;
        }
    }
}
