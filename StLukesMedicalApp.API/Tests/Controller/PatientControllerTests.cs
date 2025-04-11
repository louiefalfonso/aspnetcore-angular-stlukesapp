using Microsoft.AspNetCore.Mvc;
using Moq;
using StLukesMedicalApp.API.Controllers;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
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
        public async Task GetAllPatientsTest_Success() 
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient
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
                },
                new Patient
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
                },
            };

            // Simulate Patient Repository
            mockPatientRepository
                .Setup(repo => repo.GetAllAsync(null, null, null, 1, 100))
                .ReturnsAsync(patients);

            // Act
            var result = await patientsController.GetAllPatients(null, null, null, 1, 100);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatients = Assert.IsType<List<PatientDto>>(okResult.Value);
            Assert.Equal(2, returnedPatients.Count);

            // Verify properties of returned patients
            Assert.Equal("Emily", returnedPatients[0].FirstName);
            Assert.Equal("Johnson", returnedPatients[0].LastName);
            Assert.Equal("In Patient", returnedPatients[0].PatientType);

            // Verify properties of returned patients
            Assert.Equal("Michael", returnedPatients[1].FirstName);
            Assert.Equal("Brown", returnedPatients[1].LastName);
            Assert.Equal("Out Patient", returnedPatients[1].PatientType);
        }

        [Fact]
        public async Task GetAllPatientsTest_Failed()
        {
            // Arrange
            mockPatientRepository
                .Setup(repo => repo.GetAllAsync(null, null, null, 1, 100))
                .ReturnsAsync(new List<Patient>());

            // Act
            var result = await patientsController.GetAllPatients(null, null, null, 1, 100);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatients = Assert.IsType<List<PatientDto>>(okResult.Value);
            Assert.Empty(returnedPatients);
        }

        [Fact]
        public async Task GetPatientByIdTest_Success()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patient = new Patient
            {
                Id = patientId,
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

            // Simulate Patient Repository
            mockPatientRepository
                .Setup(repo => repo.GetByIdAsync(patientId))
                .ReturnsAsync(patient);

            // Act
            var result = await patientsController.GetPatientById(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(patientId, returnedPatient.Id);
        }

        [Fact]
        public async Task GetPatientByIdTest_Failed()
        {
            // Arrange
            var patientId = Guid.NewGuid();

            // Simulate Patient Repository returning null for a non-existent patient
            mockPatientRepository
                .Setup(repo => repo.GetByIdAsync(patientId))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await patientsController.GetPatientById(patientId);

            // Aseert
            var noPatientFound = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, noPatientFound.StatusCode);
        }

        [Fact]
        public async Task CreateNewPatientTest_Success()
        {
            // Arrange 
            var patient = new Patient
            {
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

            var request = new CreatePatientRequestDto
            {
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

            // Simulate Patient Repository
            mockPatientRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Patient>()))
                .ReturnsAsync(patient);

            // Act 
            var result = await patientsController.CreateNewPatient(request);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatient = Assert.IsType<PatientDto>(createdResult.Value);
            Assert.Equal(patient.Id, returnedPatient.Id);
        }

        [Fact]
        public async Task CreateNewPatientTest_Failed()
        {
            // Arrange
            var request = new CreatePatientRequestDto
            {
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

            // Exception being thrown by the repository
           mockPatientRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Patient>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => patientsController.CreateNewPatient(request));
            Assert.Equal("Database error", exception.Message);
        }

        [Fact]
        public async Task UpdatePatientTest_Success()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var patient = new Patient
            {
                Id = patientId,
                FirstName = "Sarah",
                LastName = "Davis",
                Email = "sarahdavis@gmail.com",
                ContactNumber = "07-123456789",
                Sex = "Female",
                Age = "29",
                Address = "456 Oak Street, Metropolis, NY 10001",
                Diagnosis = "Anxiety and mild depression - She has been feeling anxious and down for several months, affecting her daily life.",
                PatientType = "Out Patient"

            };

            var request = new UpdatePatientRequestDto
            {
                FirstName = "David",
                LastName = "Wilson",
                Email = "davidwilson@gmail.com",
                ContactNumber = "07-321654987",
                Sex = "Male",
                Age = "52",
                Address = "321 Pine Road, Smalltown, CA 90210",
                Diagnosis = "Type 2 diabetes management - He is seeking advice on managing his diabetes and maintaining a healthy lifestyle.",
                PatientType = "In Patient"
            };

            // Simulate repository and returns the updated patient with the correct Id
            mockPatientRepository
               .Setup(repo => repo.UpdateAsync(It.IsAny<Patient>()))
               .ReturnsAsync((Patient updatedPatient) =>
               {
                   updatedPatient.Id = patientId;
                   return updatedPatient;
               });

            // Act
            var result = await patientsController.UpdatePatient(patientId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(patientId, returnedPatient.Id);
            Assert.Equal("David", returnedPatient.FirstName);
            Assert.Equal("Wilson", returnedPatient.LastName);
            Assert.Equal("Type 2 diabetes management - He is seeking advice on managing his diabetes and maintaining a healthy lifestyle.", returnedPatient.Diagnosis);
            Assert.Equal("In Patient", returnedPatient.PatientType);

        }

        [Fact]
        public async Task UpdatePatientTest_Failed() 
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var request = new UpdatePatientRequestDto
            {
                FirstName = "David",
                LastName = "Wilson",
                Email = "davidwilson@gmail.com",
                ContactNumber = "07-321654987",
                Sex = "Male",
                Age = "52",
                Address = "321 Pine Road, Smalltown, CA 90210",
                Diagnosis = "Type 2 diabetes management - He is seeking advice on managing his diabetes and maintaining a healthy lifestyle.",
                PatientType = "In Patient"
            };

            // Simulate the repository returning null to indicate a failure
            mockPatientRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Patient>()))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await patientsController.UpdatePatient(patientId, request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeletePatientTest_Success()
        {
            // Arrange 
            var patientId = Guid.NewGuid();
            var patient = new Patient
            {
                Id = patientId,
                FirstName = "David",
                LastName = "Wilson",
                Email = "davidwilson@gmail.com",
                ContactNumber = "07-321654987",
                Sex = "Male",
                Age = "52",
                Address = "321 Pine Road, Smalltown, CA 90210",
                Diagnosis = "Type 2 diabetes management - He is seeking advice on managing his diabetes and maintaining a healthy lifestyle.",
                PatientType = "In Patient"
            };

            // Simulate Patient Repository
            mockPatientRepository
               .Setup(repo => repo.DeleteAsync(patientId))
               .ReturnsAsync(patient);

            // Act
            var result = await patientsController.DeletePatient(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPatient = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(patientId, returnedPatient.Id);
        }

        [Fact]
        public async Task DeletePatient_Failed()
        {
            // Arrange
            var patientId = Guid.NewGuid();

            // Simulate repository returning null to indicate the patient was not found
            mockPatientRepository
                .Setup(repo => repo.DeleteAsync(patientId))
                .ReturnsAsync((Patient?)null);

            // Act
            var result = await patientsController.DeletePatient(patientId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetPatientsTotalCount_Success()
        {
            // Arrange
            var count = 5;
           mockPatientRepository
                .Setup(repo => repo.GetCount())
                .ReturnsAsync(count);

            // Act
            var result = await patientsController.GetPatientsTotal();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCount = Assert.IsType<int>(okResult.Value);
            Assert.Equal(count, returnedCount);
        }

    }
}

