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

        // get all prescriptions
        public async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return await dbContext.Prescriptions.Include(x => x.Doctors).Include(x => x.Patients).ToListAsync();
        }

        // get prescription by ID
        public async Task<Prescription?> GetByIdAsync(Guid id)
        {
            return await dbContext.Prescriptions.Include(x => x.Doctors).Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == id);
        }

        // update prescription
        public async Task<Prescription?> UpdateAsync(Prescription prescription)
        {
            // get prescription by ID
            var existingPrescription = await dbContext.Prescriptions.Include(x => x.Doctors).Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == prescription.Id);

            // check if prescription is null
            if (existingPrescription == null) 
            {
                return null;
            }

            // update prescription
            dbContext.Entry(existingPrescription).CurrentValues.SetValues(prescription);

            // update doctor
            existingPrescription.Doctors = prescription.Doctors;

            // update patient
            existingPrescription.Patients = prescription.Patients;

            // save changes
            await dbContext.SaveChangesAsync();

            return prescription;
        }

        // delete prescription
        public async Task<Prescription?> DeleteAsync(Guid id)
        {
            // get prescription by ID
            var existingPrescription = await dbContext.Prescriptions.FirstOrDefaultAsync(x => x.Id == id);

            // check if existing prescription is null
            if (existingPrescription != null) 
            { 
                dbContext.Prescriptions.Remove(existingPrescription);
                await dbContext.SaveChangesAsync();
                return existingPrescription;
            }

            return null;

        }
    }
}