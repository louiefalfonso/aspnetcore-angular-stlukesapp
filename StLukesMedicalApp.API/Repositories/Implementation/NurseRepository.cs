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
    }
}
