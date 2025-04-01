using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Models.DTO;
using StLukesMedicalApp.API.Repositories.Implementation;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BillingsController : ControllerBase
    {
        private readonly IBillingRepository billingRepository;
        private readonly IPatientRepository patientRepository;

        // add constructor
        public BillingsController(
            IBillingRepository  billingRepository,
            IPatientRepository  patientRepository
            ) 
        {
            this.billingRepository = billingRepository;
            this.patientRepository = patientRepository;
        }

        // Add New Billing
        [HttpPost]
        public async Task<IActionResult> CreateNewBilling([FromBody] CreateBillingRequestDto request)
        {
            // Map DTO to Domain Model
            var billing = new Billing 
            { 
                TotalAmount = request.TotalAmount,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentStatus,
                DateOfBilling = request.DateOfBilling,
                Remarks = request.Remarks,
                Patients = new List<Patient>(),
            };

            // Add Patient to Billing
            foreach (var patientGuid in request.Patients)
            {
                // get patient by Id
                var exisitingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // check if patient is nulll
                if (exisitingPatient is not null)
                {
                    billing.Patients.Add(exisitingPatient);
                }
            }

            // Add New Billing
            billing = await billingRepository.CreateAsync(billing);

            // Map Domain to DTO
            var response = new BillingDto
            {
                Id = billing.Id,
                TotalAmount = billing.TotalAmount,
                PaymentMethod = billing.PaymentMethod,
                PaymentStatus = billing.PaymentStatus,
                DateOfBilling= billing.DateOfBilling,
                Remarks = billing.Remarks,
                Patients = billing.Patients.Select(x => new PatientDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Sex = x.Sex,
                    Age = x.Age,
                    Address = x.Address,
                    Diagnosis = x.Diagnosis,
                    PatientType = x.PatientType,
                }).ToList()
            };

            return Ok(response);

        }

        // Get All Billings
        [HttpGet]
        public async Task<IActionResult> GetAllBillings
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            // get all billings
            var billings = await billingRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // map domain model to DTO
            var response = new List<BillingDto>();
            {
                foreach (var billing in billings)
                {
                    response.Add(new BillingDto
                    { 
                        Id= billing.Id,
                        TotalAmount= billing.TotalAmount,
                        PaymentMethod = billing.PaymentMethod,
                        PaymentStatus = billing.PaymentStatus,  
                        DateOfBilling = billing.DateOfBilling,
                        Remarks = billing.Remarks,
                        Patients = billing.Patients.Select(x => new PatientDto
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            ContactNumber = x.ContactNumber,
                            Sex = x.Sex,
                            Age = x.Age,
                            Address = x.Address,
                            Diagnosis = x.Diagnosis,
                            PatientType = x.PatientType,
                        }).ToList()
                    });
                }
            }

            return Ok(response);
        }

        // Get all Billings by Payment Status
        [HttpGet("{paymentStatus}")]
        public async Task<ActionResult<IEnumerable<Billing>>> GetAllBillingByPaymentStatus(string paymentStatus)
        {
            var billings = await billingRepository.GetAllBillingByPaymentStatusAsync(paymentStatus);

            if (billings == null || !billings.Any())
            {
                return NotFound();
            }

            return Ok(billings);
        }

        // Get Billing By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBillingById([FromRoute] Guid id)
        {
            // get billing by Id
            var billing = await billingRepository.GetByIdAsync(id);

            // check if billing is null
            if (billing == null)
            {
                return NotFound();
            }

            // map Domain model to DTO
            var response = new BillingDto
            {
                Id = billing.Id,
                TotalAmount = billing.TotalAmount,
                PaymentMethod = billing.PaymentMethod,
                PaymentStatus = billing.PaymentStatus,
                DateOfBilling = billing.DateOfBilling,
                Remarks = billing.Remarks,
                Patients = billing.Patients.Select(x => new PatientDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Sex = x.Sex,
                    Age = x.Age,
                    Address = x.Address,
                    Diagnosis = x.Diagnosis,
                    PatientType = x.PatientType,
                }).ToList()
            };
            return Ok(response);
        }

        // Update Billing
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBilling([FromRoute] Guid id, UpdateBillingRequestDto request)
        {
            // convert DTO to domain model
            var billing = new Billing
            {
                Id = id,
                TotalAmount = request.TotalAmount,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentStatus,
                DateOfBilling = request.DateOfBilling,
                Remarks = request.Remarks,
                Patients = new List<Patient>()
            };

            // Add Patient to Billing
            foreach (var patientGuid in request.Patients)
            {
                // Get Patient By ID from Patient Repository
                var existingPatient = await patientRepository.GetByIdAsync(patientGuid);

                // Check if Patient Exist
                if (existingPatient is not null)
                {
                    billing.Patients.Add(existingPatient);
                }
            }

            // Call Repository to update Billing Domain Model
            var updatedBilling = await billingRepository.UpdateAsync(billing);

            // Check if Updated Appointment is Null
            if (updatedBilling == null)
            {
                return NotFound();
            }

            // map Domain model to DTO
            var response = new BillingDto
            {
                Id = billing.Id,
                TotalAmount = billing.TotalAmount,
                PaymentMethod = billing.PaymentMethod,
                PaymentStatus = billing.PaymentStatus,
                DateOfBilling = billing.DateOfBilling,
                Remarks = billing.Remarks,
                Patients = billing.Patients.Select(x => new PatientDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNumber = x.ContactNumber,
                    Sex = x.Sex,
                    Age = x.Age,
                    Address = x.Address,
                    Diagnosis = x.Diagnosis,
                    PatientType = x.PatientType,
                }).ToList()
            };

            return Ok(response);

        }

        // Delete Billing
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBilling([FromRoute] Guid id)
        {
            // delete billing
            var deletedBilling = await billingRepository.DeleteAsync(id);

            // check if deleted billing is null
            if (deletedBilling == null)
            {
                return NotFound();
            }

            // convert domain model to DTO
            var response = new BillingDto
            {
                Id = deletedBilling.Id,
                TotalAmount = deletedBilling.TotalAmount,
                PaymentMethod = deletedBilling.PaymentMethod,
                PaymentStatus = deletedBilling.PaymentStatus,
                DateOfBilling = deletedBilling.DateOfBilling,
                Remarks = deletedBilling.Remarks,
            };
            return Ok(response);
        }

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetPatientsTotal()
        {
            var count = await billingRepository.GetCount();
            return Ok(count);
        }
    }
}