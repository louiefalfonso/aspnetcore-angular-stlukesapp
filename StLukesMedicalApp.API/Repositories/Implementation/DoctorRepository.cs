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
        public async Task<IEnumerable<Doctor>> GetAllAsync
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
            var doctors = dbContext.Doctors.AsQueryable();


            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                doctors = doctors.Where(
                    x => x.FirstName.Contains(query) ||
                    x.LastName.Contains(query) ||
                    x.Specialization.Contains(query) ||
                    x.Department.Contains(query));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "firstName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    doctors = isAsc ? doctors.OrderBy(x => x.FirstName) : doctors.OrderByDescending(x => x.FirstName);
                }

                if (string.Equals(sortBy, "lastName", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    doctors = isAsc ? doctors.OrderBy(x => x.LastName) : doctors.OrderByDescending(x => x.LastName);
                }

                if (string.Equals(sortBy, "specialization", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    doctors = isAsc ? doctors.OrderBy(x => x.Specialization) : doctors.OrderByDescending(x => x.Specialization);
                }

                if (string.Equals(sortBy, "department", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    doctors = isAsc ? doctors.OrderBy(x => x.Department) : doctors.OrderByDescending(x => x.Department);
                }
            }


                // pagination
                var skipResults = (pageNumber - 1) * pageSize;
            doctors = doctors.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await doctors.ToListAsync();
        }

        // Get Doctor By ID
        public async Task<Doctor?> GetByIdAsync(Guid id)
        {
            return await dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Update Doctor
        public async Task<Doctor?> UpdateAsync(Doctor doctor)
        {
            var existingDoctor = await dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == doctor.Id);

            if (existingDoctor != null)
            {
                dbContext.Entry(existingDoctor).CurrentValues.SetValues(doctor);
                await dbContext.SaveChangesAsync();
                return doctor;
            }

            return null;
        }

        // Delete Doctor
        public async Task<Doctor?> DeleteAsync(Guid id)
        {
            // Get existing Doctor By ID
            var existingDoctor = await dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);

            // Check if Exixsting Doctor is null
            if (existingDoctor is null)
            {
                return null;
            }

            // Delete Doctor & Save Changes
            dbContext.Doctors.Remove(existingDoctor);
            await dbContext.SaveChangesAsync();
            return existingDoctor;
        }

        // Get All Doctors By Department
        public async Task<IEnumerable<Doctor>> GetAllDoctorsByDepartment(string department)
        {
            return await dbContext.Doctors.Where(b => b.Department.ToLower() == department.ToLower()).ToListAsync();
        }

        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Doctors.CountAsync();
        }
    }
}


