using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Implementation;
using Xunit;

namespace StLukesMedicalApp.API.Tests.Repository
{
    public class AdmissionRepositoryTests
    {
        private readonly AdmissionRepository admissionRepository;
        private readonly ApplicationDbContext dbContext;
        public AdmissionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated(); 
            admissionRepository = new AdmissionRepository(dbContext);
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task CreateAsync_ShouldAddAdmission()
        {
            // Arrange
            var admission = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Remarks = "Patient Already Admitted",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            // Act
            var result = await admissionRepository.CreateAsync(admission);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(admission.Id, result.Id);
            Assert.Equal(1, await admissionRepository.GetCount());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAdmissions()
        {
            // Arrange
            var admission1 = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "101",
                RoomType = "Single", 
                AvailabilityStatus = "Available",
                Remarks = "Test Admission 1", 
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            var admission2 = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "102",
                RoomType = "Double",
                AvailabilityStatus = "Occupied", 
                Remarks = "Test Admission 2", 
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            await admissionRepository.CreateAsync(admission1);
            await admissionRepository.CreateAsync(admission2);

            // Act
            var result = await admissionRepository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAdmission()
        {
            // Arrange
            var admission = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "101",
                RoomType = "Single", // Required property
                AvailabilityStatus = "Available", // Required property
                Remarks = "Test Admission", // Required property
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            await admissionRepository.CreateAsync(admission);

            // Act
            var result = await admissionRepository.GetByIdAsync(admission.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(admission.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAdmission()
        {
            // Arrange
            var admission = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Remarks = "Initial Remarks",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            // Add the admission to the database
            await admissionRepository.CreateAsync(admission);

            // Modify the admission
            admission.RoomNumber = "102";
            admission.RoomType = "Double";
            admission.AvailabilityStatus = "Occupied";
            admission.Remarks = "Updated Remarks";

            // Act
            var result = await admissionRepository.UpdateAsync(admission);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("102", result.RoomNumber);
            Assert.Equal("Double", result.RoomType);
            Assert.Equal("Occupied", result.AvailabilityStatus);
            Assert.Equal("Updated Remarks", result.Remarks);

            // Verify the changes in the database
            var updatedAdmission = await admissionRepository.GetByIdAsync(admission.Id);
            Assert.NotNull(updatedAdmission);
            Assert.Equal("102", updatedAdmission.RoomNumber);
            Assert.Equal("Double", updatedAdmission.RoomType);
            Assert.Equal("Occupied", updatedAdmission.AvailabilityStatus);
            Assert.Equal("Updated Remarks", updatedAdmission.Remarks);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAdmission()
        {
            // Arrange
            var admission = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Remarks = "Test Admission",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            // Add the admission to the database
            await admissionRepository.CreateAsync(admission);

            // Act
            var result = await admissionRepository.DeleteAsync(admission.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(admission.Id, result.Id);

            // Verify that the admission was removed from the database
            var deletedAdmission = await admissionRepository.GetByIdAsync(admission.Id);
            Assert.Null(deletedAdmission);
        }


        [Fact]
        public async Task GetCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var admission1 = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Remarks = "Test Admission 1",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            var admission2 = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "102",
                RoomType = "Double",
                AvailabilityStatus = "Occupied",
                Remarks = "Test Admission 2",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            // Add the admissions to the database
            await admissionRepository.CreateAsync(admission1);
            await admissionRepository.CreateAsync(admission2);

            // Act
            var count = await admissionRepository.GetCount();

            // Assert
            Assert.Equal(2, count);
        }

    }
}
