using DatingAppAPI.Entities;

namespace DatingAppAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToekn(AppUser appUser);
    }
}
