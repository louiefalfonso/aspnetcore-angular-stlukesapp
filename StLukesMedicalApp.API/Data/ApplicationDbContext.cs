using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
            public DbSet<Patient> Patients { get; set; }
            public DbSet<Doctor> Doctors { get; set; }

    }
}
