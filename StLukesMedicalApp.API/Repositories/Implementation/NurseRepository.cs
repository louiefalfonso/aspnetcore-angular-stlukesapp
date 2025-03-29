using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class NurseRepository : INurseRepository
    {
        private readonly ApplicationDbContext dbContext;

        // Create Constructor
        public NurseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create New Nurse
        public async Task<Nurse> CreateAsync(Nurse nurse)
        {
           await dbContext.Nurses.AddAsync(nurse);
           await dbContext.SaveChangesAsync();
           return nurse;
        }

        // Get Nurse By Id
        public async Task<Nurse?> GetByIdAsync(Guid id)
        {
            return await dbContext.Nurses.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Get All Nurses
        public async Task<IEnumerable<Nurse>> GetAllAsync()
        {
            return await dbContext.Nurses.ToListAsync();
        }

        // Update Nurse
        public async Task<Nurse?> UpdateAsync(Nurse nurse)
        {
           // Get Nurse By ID
           var existingNurse = dbContext.Nurses.FirstOrDefault( x=> x.Id == nurse.Id);

            // Check if Nurse is Null
            if (existingNurse != null) 
            {
                dbContext.Entry(existingNurse).CurrentValues.SetValues(nurse);
                await dbContext.SaveChangesAsync();
                return nurse;
            }
            return null;
        }

        // Delete Nurse
        public async Task<Nurse?> DeleteAsync(Guid id)
        {
            // Get Nurse By ID
            var existingNurse = dbContext.Nurses.FirstOrDefault(x => x.Id == id);

            if (existingNurse != null) 
            { 
                dbContext.Nurses.Remove(existingNurse);
                await dbContext.SaveChangesAsync();
                return existingNurse;
            }
            return null;
        }


        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Nurses.CountAsync();
        }
    }
}
