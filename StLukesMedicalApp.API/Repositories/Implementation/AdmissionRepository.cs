using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly ApplicationDbContext dbContext;

        // create constructor
        public AdmissionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // create new admission
        public async Task<Admission> CreateAsync(Admission admission)
        {
            await dbContext.Admissions.AddAsync(admission);
            await dbContext.SaveChangesAsync();
            return admission;
        }

        // get all admissions
        public async Task<IEnumerable<Admission>> GetAllAsync
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
            var admissions = dbContext.Admissions.AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                admissions = admissions.Where(x =>
                    x.RoomNumber.Contains(query) ||
                    x.RoomType.Contains(query));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (string.Equals(sortBy, "roomNumber", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    admissions = isAsc ? admissions.OrderBy(x => x.RoomNumber) : admissions.OrderByDescending(x => x.RoomNumber);
                }

                if (string.Equals(sortBy, "roomType", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    admissions = isAsc ? admissions.OrderBy(x => x.RoomType) : admissions.OrderByDescending(x => x.RoomType);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            admissions = admissions.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await admissions.Include(x => x.Doctors).Include(x => x.Patients).Include(x => x.Nurses).ToListAsync();
        }

        // get admission by ID
        public async Task<Admission?> GetByIdAsync(Guid id)
        {
            return await dbContext.Admissions.Include(x => x.Doctors).Include(x => x.Patients).Include(x=> x.Nurses).FirstOrDefaultAsync(x => x.Id == id);
        }

        // update admission
        public async Task<Admission?> UpdateAsync(Admission admission)
        {
            // fetch the admission by ID
            var existingAdmission = dbContext.Admissions.Include(x => x.Doctors).Include(x => x.Patients).Include(x => x.Nurses).FirstOrDefault(x => x.Id == admission.Id);

            // check if existing admission is null
            if (existingAdmission == null)
            {
                return null;
            }

            // update admission
            dbContext.Entry(existingAdmission).CurrentValues.SetValues(admission);

            // update doctor
            existingAdmission.Doctors = admission.Doctors;

            // update patient
            existingAdmission.Patients = admission.Patients;

            // update nurse
            existingAdmission.Nurses = admission.Nurses;

            // save changes
            await dbContext.SaveChangesAsync();

            return existingAdmission;
        }

        // delete admission
        public async Task<Admission?> DeleteAsync(Guid id)
        {
            // fetch the admission by ID
            var existingAdmission = await dbContext.Admissions.FirstOrDefaultAsync(x => x.Id == id);

            // check if existing admission is Null
            if (existingAdmission != null)
            {
                dbContext.Admissions.Remove(existingAdmission);
                await dbContext.SaveChangesAsync();
                return existingAdmission;
            }

            return null;
        }

        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Admissions.CountAsync();
        }

    }
}