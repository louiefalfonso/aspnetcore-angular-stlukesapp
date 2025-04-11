using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Implementation;
using Xunit;

namespace StLukesMedicalApp.API.Tests.Repository
{
    public class PatientRepositoryTests
    {
        private readonly PatientRepository patientRepository;
        private readonly ApplicationDbContext dbContext;

        public PatientRepositoryTests() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            patientRepository = new PatientRepository(dbContext);
        }

        [Fact]
        public async Task CreateInBulkAsync_ShouldAddPatients()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    ContactNumber = "1234567890",
                    Sex = "Male",
                    Age = "30",
                    Address = "123 Main St",
                    Diagnosis = "Flu",
                    PatientType = "Outpatient"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    ContactNumber = "0987654321",
                    Sex = "Female",
                    Age = "25",
                    Address = "456 Elm St",
                    Diagnosis = "Cold",
                    PatientType = "Inpatient"
                }
            };

            var dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("StLukesAppDB").Options);

            var repository = new PatientRepository(dbContext);

            // Act
            var result = await repository.CreateInBulkAsync(patients);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(2, await dbContext.Patients.CountAsync());
        }
    }
}
