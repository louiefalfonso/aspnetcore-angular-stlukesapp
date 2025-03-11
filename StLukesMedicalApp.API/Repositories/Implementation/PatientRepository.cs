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
    }
}
