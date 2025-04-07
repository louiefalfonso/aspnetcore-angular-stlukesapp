using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Models.Domain;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class BillingRepository : IBillingRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BillingRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // create new biiling
        public async Task<Billing> CreateAsync(Billing billing)
        {
            await dbContext.Billings.AddAsync(billing);
            await dbContext.SaveChangesAsync();
            return billing;
        }

        // get billing by ID
        public async Task<Billing?> GetByIdAsync(Guid id)
        {
            return await dbContext.Billings.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == id);
        }

        // get all billings
        public async Task<IEnumerable<Billing>> GetAllAsync
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
            var billings = dbContext.Billings.AsQueryable();

            // filtering
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                billings = billings.Where(x =>
                    x.PaymentMethod.Contains(query) ||
                    x.PaymentStatus.Contains(query) ||
                    x.Patients.Any(p => p.FirstName.Contains(query) || p.LastName.Contains(query)) ||
                    x.TotalAmount.Contains(query));
            }

            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "totalAmount", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    billings = isAsc ? billings.OrderBy(x => x.TotalAmount) : billings.OrderByDescending(x => x.TotalAmount);
                }

                if (string.Equals(sortBy, "paymentStatus", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    billings = isAsc ? billings.OrderBy(x => x.PaymentStatus) : billings.OrderByDescending(x => x.PaymentStatus);
                }

                if (string.Equals(sortBy, "paymentMethod", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    billings = isAsc ? billings.OrderBy(x => x.PaymentMethod) : billings.OrderByDescending(x => x.PaymentMethod);
                }


            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            billings = billings.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await billings.Include(x => x.Patients).ToListAsync();
        }

        // update billing
        public async Task<Billing?> UpdateAsync(Billing billing)
        {
            // get billing by ID
            var existingBilling = await dbContext.Billings.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == billing.Id);

            // check if billing is null
            if (existingBilling == null) 
            {
                return null;
            }

            // update billing
            dbContext.Entry(existingBilling).CurrentValues.SetValues(billing);

            // update Patient
            existingBilling.Patients = billing.Patients;

            // save changes
            await dbContext.SaveChangesAsync();

            return existingBilling;
        }

        // delete biiling
        public async Task<Billing?> DeleteAsync(Guid id)
        {
            // get billing by ID
            var existingBilling = await dbContext.Billings.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == id);

            // check if existing billing is null
            if (existingBilling != null) 
            { 
                dbContext.Billings.Remove(existingBilling);
                await dbContext.SaveChangesAsync();
                return existingBilling;
            }
            return null;
        }

        // get all billings by payment status
        public async Task<IEnumerable<Billing>> GetAllBillingByPaymentStatusAsync(string paymentStatus)
        {
            return await dbContext.Billings.Where(b => b.PaymentStatus.Equals(paymentStatus, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        // Get Count
        public async Task<int> GetCount()
        {
            return await dbContext.Billings.CountAsync();
        }
    }
}