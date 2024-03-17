using VinylX.Models;

namespace VinylX.Services
{
    public interface IUserService
    {
        Task<User?> GetLoggedInUser();
    }
}
