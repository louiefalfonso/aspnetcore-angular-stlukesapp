using Microsoft.AspNetCore.Mvc;
using Moq;
using StLukesMedicalApp.API.Controllers;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Interface;
using Xunit;

namespace StLukesMedicalApp.API.Tests.Controller
{
    public class AdmissionControllerTests
    {
        private readonly Mock<IAdmissionRepository> mockAdmissionRepository;
        private readonly Mock<IDoctorRepository> mockDoctorRepository;
        private readonly Mock<INurseRepository> mockNurseRepository;
        private readonly Mock<IPatientRepository> mockPatientRepository;
        private readonly AdmissionsController admissionsController;

        public AdmissionControllerTests()
        {
            mockAdmissionRepository = new Mock<IAdmissionRepository>();
            mockDoctorRepository = new Mock<IDoctorRepository>();
            mockNurseRepository = new Mock<INurseRepository>();
            mockPatientRepository = new Mock<IPatientRepository>();

            admissionsController = new AdmissionsController(
                mockAdmissionRepository.Object,
                mockDoctorRepository.Object,
                mockNurseRepository.Object,
                mockPatientRepository.Object
            );
        }

        [Fact]
        public async Task GetAllAdmissions_ShouldReturn_Success()
        {
            // Arrange
            var admissions = new List<Admission>
            {
                new Admission
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = "101",
                    RoomType = "Single",
                    AvailabilityStatus = "Available",
                    Date = DateTime.UtcNow,
                    Remarks = "Test Admission 1",
                    Doctors = new List<Doctor>(),
                    Patients = new List<Patient>(),
                    Nurses = new List<Nurse>()
                },
                new Admission
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = "102",
                    RoomType = "Double",
                    AvailabilityStatus = "Occupied",
                    Date = DateTime.UtcNow,
                    Remarks = "Test Admission 2",
                    Doctors = new List<Doctor>(),
                    Patients = new List<Patient>(),
                    Nurses = new List<Nurse>()
                }
            };

            mockAdmissionRepository
                .Setup(repo => repo.GetAllAsync(null, null, null, 1, 100))
                .ReturnsAsync(admissions);

            // Act
            var result = await admissionsController.GetAllAdmissions(null, null, null, 1, 100);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAdmissions = Assert.IsType<List<AdmissionDto>>(okResult.Value);
            Assert.Equal(2, returnedAdmissions.Count);

            // Verify the properties of the returned admissions
            Assert.Equal("101", returnedAdmissions[0].RoomNumber);
            Assert.Equal("Single", returnedAdmissions[0].RoomType);
            Assert.Equal("Available", returnedAdmissions[0].AvailabilityStatus);

            Assert.Equal("102", returnedAdmissions[1].RoomNumber);
            Assert.Equal("Double", returnedAdmissions[1].RoomType);
            Assert.Equal("Occupied", returnedAdmissions[1].AvailabilityStatus);
        }

        [Fact]
        public async Task GetAdmissionById_ShouldReturn_Success()
        {
            // Arrange
            var admissionId = Guid.NewGuid();
            var admission = new Admission
            {
                Id = admissionId,
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Date = DateTime.UtcNow,
                Remarks = "Test Admission",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            mockAdmissionRepository
                .Setup(repo => repo.GetByIdAsync(admissionId))
                .ReturnsAsync(admission);

            // Act
            var result = await admissionsController.GetAdmissionById(admissionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAdmission = Assert.IsType<AdmissionDto>(okResult.Value);
            Assert.Equal(admissionId, returnedAdmission.Id);
        }

        [Fact]
        public async Task CreateNewAdmission_ShouldReturn_Success()
        {
            // Arrange
            var admission = new Admission
            {
                Id = Guid.NewGuid(),
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Date = DateTime.UtcNow,
                Remarks = "Test Admission",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            var request = new CreateAdmissionRequestDto
            {
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Remarks = "Test Admission",
                Date = DateTime.UtcNow,
                Doctors = Array.Empty<Guid>(),
                Patients = Array.Empty<Guid>(),
                Nurses = Array.Empty<Guid>()
            };

            mockAdmissionRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Admission>()))
                .ReturnsAsync(admission);

            // Act
            var result = await admissionsController.CreateNewAdmission(request);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var returnedAdmission = Assert.IsType<AdmissionDto>(createdResult.Value);
            Assert.Equal(admission.Id, returnedAdmission.Id);
        }

        [Fact]
        public async Task UpdateAdmission_ShouldReturn_Success()
        {
            // Arrange
            var admissionId = Guid.NewGuid();
            var admission = new Admission
            {
                Id = admissionId,
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Date = DateTime.UtcNow,
                Remarks = "Initial Remarks",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            var request = new UpdateAdmissionRequestDto
            {
                RoomNumber = "102",
                RoomType = "Double",
                AvailabilityStatus = "Occupied",
                Remarks = "Updated Remarks",
                Date = DateTime.UtcNow,
                Doctors = new List<Guid>(),
                Patients = new List<Guid>(),
                Nurses = new List<Guid>()
            };

            // Ensure the mock returns the updated admission with the correct Id
            mockAdmissionRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Admission>()))
                .ReturnsAsync((Admission updatedAdmission) =>
                {
                    updatedAdmission.Id = admissionId; 
                    return updatedAdmission;
                });

            // Act
            var result = await admissionsController.UpdateAdmission(admissionId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAdmission = Assert.IsType<AdmissionDto>(okResult.Value);
            Assert.Equal(admissionId, returnedAdmission.Id);
            Assert.Equal("102", returnedAdmission.RoomNumber);
            Assert.Equal("Double", returnedAdmission.RoomType);
            Assert.Equal("Occupied", returnedAdmission.AvailabilityStatus);
            Assert.Equal("Updated Remarks", returnedAdmission.Remarks);
        }

        [Fact]
        public async Task DeleteAdmission_ShouldReturn_Success()
        {
            // Arrange
            var admissionId = Guid.NewGuid();
            var admission = new Admission
            {
                Id = admissionId,
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Date = DateTime.UtcNow,
                Remarks = "Test Admission",
                Doctors = new List<Doctor>(),
                Patients = new List<Patient>(),
                Nurses = new List<Nurse>()
            };

            mockAdmissionRepository
                .Setup(repo => repo.DeleteAsync(admissionId))
                .ReturnsAsync(admission);

            // Act
            var result = await admissionsController.DeleteAdmission(admissionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAdmission = Assert.IsType<AdmissionDto>(okResult.Value);
            Assert.Equal(admissionId, returnedAdmission.Id);
        }

        [Fact]
        public async Task GetCount_ShouldReturn_Success()
        {
            // Arrange
            var count = 5;
            mockAdmissionRepository
                .Setup(repo => repo.GetCount())
                .ReturnsAsync(count);

            // Act
            var result = await admissionsController.GetAdmissionsTotal();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCount = Assert.IsType<int>(okResult.Value);
            Assert.Equal(count, returnedCount);
        }

        [Fact]
        public async Task GetAllAdmissions_ShouldReturn_Failed()
        {
            // Arrange
            mockAdmissionRepository
                .Setup(repo => repo.GetAllAsync(null, null, null, 1, 100))
                .ReturnsAsync(new List<Admission>()); // Return an empty list instead of null

            // Act
            var result = await admissionsController.GetAllAdmissions(null, null, null, 1, 100);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Controller should return an OkObjectResult
            var returnedAdmissions = Assert.IsType<List<AdmissionDto>>(okResult.Value); // Ensure the result is a list
            Assert.Empty(returnedAdmissions); // Verify that the returned list is empty
        }

        [Fact]
        public async Task GetAdmissionById_ShouldReturn_Failed()
        {
            // Arrange
            var admissionId = Guid.NewGuid();

            // Simulate the repository returning null for a non-existent admission
            mockAdmissionRepository
                .Setup(repo => repo.GetByIdAsync(admissionId))
                .ReturnsAsync((Admission?)null);

            // Act
            var result = await admissionsController.GetAdmissionById(admissionId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result); // Ensure the result is a 404 NotFound
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode); // Verify the status code
        }

        [Fact]
        public async Task CreateNewAdmission_ShouldThrow_Exception()
        {
            // Arrange
            var request = new CreateAdmissionRequestDto
            {
                RoomNumber = "101",
                RoomType = "Single",
                AvailabilityStatus = "Available",
                Remarks = "Test Admission",
                Date = DateTime.UtcNow,
                Doctors = Array.Empty<Guid>(),
                Patients = Array.Empty<Guid>(),
                Nurses = Array.Empty<Guid>()
            };

            // exception being thrown by the repository
            mockAdmissionRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Admission>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => admissionsController.CreateNewAdmission(request));
            Assert.Equal("Database error", exception.Message);
        }

        [Fact]
        public async Task UpdateAdmission_ShouldReturn_Failed()
        {
            // Arrange
            var admissionId = Guid.NewGuid();
            var request = new UpdateAdmissionRequestDto
            {
                RoomNumber = "102",
                RoomType = "Double",
                AvailabilityStatus = "Occupied",
                Remarks = "Updated Remarks",
                Date = DateTime.UtcNow,
                Doctors = new List<Guid>(),
                Patients = new List<Guid>(),
                Nurses = new List<Guid>()
            };

            // Simulate the repository returning null to indicate a failure
            mockAdmissionRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Admission>()))
                .ReturnsAsync((Admission?)null);

            // Act
            var result = await admissionsController.UpdateAdmission(admissionId, request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result); // Ensure the result is a 404 NotFound
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode); // Verify the status code
        }

        [Fact]
        public async Task DeleteAdmission_ShouldReturn_Failed()
        {
            // Arrange
            var admissionId = Guid.NewGuid();

            // repository returning null to indicate the admission was not found
            mockAdmissionRepository
                .Setup(repo => repo.DeleteAsync(admissionId))
                .ReturnsAsync((Admission?)null);

            // Act
            var result = await admissionsController.DeleteAdmission(admissionId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result); // Ensure the result is a 404 NotFound
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode); // Verify the status code
        }

        [Fact]
        public async Task GetCount_ShouldReturn_Failed()
        {
            // Arrange & Simulate an exception being thrown by the repository
            mockAdmissionRepository
                .Setup(repo => repo.GetCount())
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => admissionsController.GetAdmissionsTotal());

            // Verify the exception message
            Assert.Equal("Database error", exception.Message); 
        }

    }
}
