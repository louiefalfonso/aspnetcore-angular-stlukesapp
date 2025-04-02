using Microsoft.AspNetCore.Mvc;
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
        public async Task<IEnumerable<Prescription>> GetAllAsync
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
            var prescriptions = dbContext.Prescriptions.AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                prescriptions = prescriptions.Where(x => x.MedicationList.Contains(query));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "MedicationList", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    prescriptions = isAsc ? prescriptions.OrderBy(x => x.MedicationList) : prescriptions.OrderByDescending(x => x.MedicationList);
                }

            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            prescriptions = prescriptions.Skip(skipResults ?? 0).Take(pageSize ?? 100);

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


        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Prescriptions.CountAsync();
        }
    }
}