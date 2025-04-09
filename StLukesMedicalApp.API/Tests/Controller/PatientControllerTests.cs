using Microsoft.AspNetCore.Mvc;
using Moq;
using StLukesMedicalApp.API.Controllers;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;
using Xunit;

namespace StLukesMedicalApp.API.Tests.Controller
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientRepository> mockPatientRepository;
        private readonly PatientsController patientsController;

        public PatientControllerTests()
        {
            mockPatientRepository = new Mock<IPatientRepository>();

            patientsController = new PatientsController(
                mockPatientRepository.Object
            );
        }

        [Fact]
        public async Task CreatePatientInBulk_ShouldReturn_Success()
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
                    PatientType = "Outpatient",
                    Billings = new List<Billing>(),
                    Admissions = new List<Admission>(),
                    Appointments = new List<Appointment>(),
                    Prescriptions = new List<Prescription>()
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
                    PatientType = "Inpatient",
                    Billings = new List<Billing>(),
                    Admissions = new List<Admission>(),
                    Appointments = new List<Appointment>(),
                    Prescriptions = new List<Prescription>()
                }
            };

            mockPatientRepository
                .Setup(repo => repo.CreateInBulkAsync(It.IsAny<IEnumerable<Patient>>()))
                .ReturnsAsync(patients);

            // Act
            var result = await patientsController.CreatePatientsInBulk(patients);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatients = Assert.IsType<List<Patient>>(okResult.Value);
            Assert.Equal(2, returnedPatients.Count);
            Assert.Equal("John", returnedPatients[0].FirstName);
            Assert.Equal("Jane", returnedPatients[1].FirstName);

            // Verify that the repository method was called once
            mockPatientRepository.Verify(repo => repo.CreateInBulkAsync(It.IsAny<IEnumerable<Patient>>()), Times.Once);
        }


        [Fact]
        public async Task CreatePatientsInBulk_ShouldReturn_BadRequest_WhenPatientsIsEmpty()
        {
            // Arrange
            var patients = new List<Patient>();

            // Act
            var result = await patientsController.CreatePatientsInBulk(patients);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Patients collection cannot be null or empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreatePatientsInBulk_ShouldReturn_BadRequest_WhenRepositoryThrowsException()
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
                    PatientType = "Outpatient",
                    Billings = new List<Billing>(),
                    Admissions = new List<Admission>(),
                    Appointments = new List<Appointment>(),
                    Prescriptions = new List<Prescription>()
                }
            };

            mockPatientRepository
                .Setup(repo => repo.CreateInBulkAsync(It.IsAny<IEnumerable<Patient>>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await patientsController.CreatePatientsInBulk(patients);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("An error occurred while creating patients in bulk.", badRequestResult.Value);

            // Verify that the repository method was called once
            mockPatientRepository.Verify(repo => repo.CreateInBulkAsync(It.IsAny<IEnumerable<Patient>>()), Times.Once);
        }


    }
}
