using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Implementation;
using StLukesMedicalApp.API.Repositories.Interface;
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

        [Fact]
        public async Task CreateAsync_Success()
        {
            // Arrange
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emilyjohnson@gmail.co",
                ContactNumber = "07-123456789",
                Sex = "Female",
                Age = "62",
                Address = "123 Elm Street, Springfield, IL 62704",
                Diagnosis = "Moderate fatigue and occasional headaches - She has been experiencing tiredness and mild headaches for the past few weeks.",
                PatientType = "In Patient"
            };

            // Act
            var result = await patientRepository.CreateAsync(patient);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patient.Id, result.Id);
            Assert.Equal(1, await patientRepository.GetCount());
        }

        [Fact]
        public async Task GetAllAsync_Success()
        {
            // Arrange
            var patient1 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emilyjohnson@gmail.co",
                ContactNumber = "07-123456789",
                Sex = "Female",
                Age = "62",
                Address = "123 Elm Street, Springfield, IL 62704",
                Diagnosis = "Moderate fatigue and occasional headaches - She has been experiencing tiredness and mild headaches for the past few weeks.",
                PatientType = "In Patient"
            };

            var patient2 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Michael",
                LastName = "Brown",
                Email = "michaelbrown@gmail.com",
                ContactNumber = "07-987654321",
                Sex = "Male",
                Age = "45",
                Address = "789 Maple Avenue, Anytown, TX 75001",
                Diagnosis = "Severe back pain and limited mobility - He has been experiencing intense pain in his lower back and difficulty in movement.",
                PatientType = "Out Patient"
            };

            // Add the patients to the database
            await patientRepository.CreateAsync(patient1);
            await patientRepository.CreateAsync(patient2);

            // Act
            var result = await patientRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // Arrange
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emilyjohnson@gmail.co",
                ContactNumber = "07-123456789",
                Sex = "Female",
                Age = "62",
                Address = "123 Elm Street, Springfield, IL 62704",
                Diagnosis = "Moderate fatigue and occasional headaches - She has been experiencing tiredness and mild headaches for the past few weeks.",
                PatientType = "In Patient"
            };

            // Add the patient to the database
            await patientRepository.CreateAsync(patient);

            // Act
            var result = await patientRepository.GetByIdAsync(patient.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patient.Id, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // Arrange
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emilyjohnson@gmail.co",
                ContactNumber = "07-123456789",
                Sex = "Female",
                Age = "62",
                Address = "123 Elm Street, Springfield, IL 62704",
                Diagnosis = "Moderate fatigue and occasional headaches - She has been experiencing tiredness and mild headaches for the past few weeks.",
                PatientType = "In Patient"
            };

            // Add the patient to the database
            await patientRepository.CreateAsync(patient);

            // Act
            var result = await patientRepository.DeleteAsync(patient.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patient.Id, result.Id);

            // Verify that the patient was removed from the database
            var deletedPatient = await patientRepository.GetByIdAsync(patient.Id);
            Assert.Null(deletedPatient);
        }

        [Fact]
        public async Task GetPatientTotal_Success()
        {
            // Arrange
            var patient1 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emilyjohnson@gmail.co",
                ContactNumber = "07-123456789",
                Sex = "Female",
                Age = "62",
                Address = "123 Elm Street, Springfield, IL 62704",
                Diagnosis = "Moderate fatigue and occasional headaches - She has been experiencing tiredness and mild headaches for the past few weeks.",
                PatientType = "In Patient"
            };

            var patient2 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Michael",
                LastName = "Brown",
                Email = "michaelbrown@gmail.com",
                ContactNumber = "07-987654321",
                Sex = "Male",
                Age = "45",
                Address = "789 Maple Avenue, Anytown, TX 75001",
                Diagnosis = "Severe back pain and limited mobility - He has been experiencing intense pain in his lower back and difficulty in movement.",
                PatientType = "Out Patient"
            };


            // Add the patients to the database
            await patientRepository.CreateAsync(patient1);
            await patientRepository.CreateAsync(patient2);

            // Act
            var count = await patientRepository.GetCount();

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task CreateAsync_WhenPatientIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => patientRepository.CreateAsync(null));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenPatientDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await patientRepository.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenPatientDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await patientRepository.DeleteAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldFail_WhenPatientDoesNotExist()
        {
            // Arrange
            var nonExistentPatient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "NonExistent",
                LastName = "Patient",
                Email = "nonexistent@example.com",
                ContactNumber = "0000000000",
                Sex = "Unknown",
                Age = "0",
                Address = "Nowhere",
                Diagnosis = "None",
                PatientType = "Unknown"
            };

            // Act
            var result = await patientRepository.UpdateAsync(nonExistentPatient);

            // Assert
            Assert.Null(result);
        }


    }
}
