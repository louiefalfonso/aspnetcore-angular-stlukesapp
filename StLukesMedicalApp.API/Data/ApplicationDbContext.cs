using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options) { }
            public DbSet<Patient> Patients { get; set; }
            public DbSet<Doctor> Doctors { get; set; }
            public DbSet<Appointment> Appointments { get; set; }
            public DbSet<Nurse> Nurses { get; set; }
            public DbSet<Prescription> Prescriptions { get; set; }
            public DbSet<Billing> Billings { get; set; }
            public DbSet<Admission> Admissions { get; set; }
    }
}
