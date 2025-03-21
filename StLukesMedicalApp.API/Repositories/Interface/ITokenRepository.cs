using Microsoft.AspNetCore.Identity;

namespace StLukesMedicalApp.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
