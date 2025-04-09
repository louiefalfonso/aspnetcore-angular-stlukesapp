using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Models.Domain;

namespace StLukesMedicalApp.API.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Admission> Admissions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
