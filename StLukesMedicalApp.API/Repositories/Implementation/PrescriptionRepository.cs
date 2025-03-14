using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext dbContext;

        //  create construstor
        public PrescriptionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // create new prescription
        public async Task<Prescription> CreateAsync(Prescription prescription)
        {
            await dbContext.Prescriptions.AddAsync(prescription);
            await dbContext.SaveChangesAsync();
            return prescription;
        }

        // get prescription by ID
        public async Task<Prescription?> GetByIdAsync(Guid id)
        {
            return await dbContext.Prescriptions.Include(x => x.Doctors).Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}