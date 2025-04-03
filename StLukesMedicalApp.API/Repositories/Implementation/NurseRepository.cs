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
        public async Task<IEnumerable<Nurse>> GetAllAsync
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
            var nurses = dbContext.Nurses.AsQueryable();


            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                nurses = nurses.Where(
                    x => x.FirstName.Contains(query) ||
                    x.LastName.Contains(query) ||
                    x.BadgeNumber.Contains(query) ||
                    x.Department.Contains(query));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "firstName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    nurses= isAsc ? nurses.OrderBy(x => x.FirstName) : nurses.OrderByDescending(x => x.FirstName);
                }

                if (string.Equals(sortBy, "lastName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    nurses = isAsc ? nurses.OrderBy(x => x.LastName) : nurses.OrderByDescending(x => x.LastName);
                }

                if (string.Equals(sortBy, "department", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    nurses = isAsc ? nurses.OrderBy(x => x.Department) : nurses.OrderByDescending(x => x.Department);
                }

                if (string.Equals(sortBy, "badgeNumber", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    nurses = isAsc ? nurses.OrderBy(x => x.BadgeNumber) : nurses.OrderByDescending(x => x.BadgeNumber);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            nurses = nurses.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await nurses.ToListAsync();
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
