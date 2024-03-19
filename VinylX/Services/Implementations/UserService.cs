using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VinylX.Data;
using VinylX.Models;

namespace VinylX.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly VinylXContext vinylXContext;

        public UserService(IHttpContextAccessor httpContextAccessor, VinylXContext vinylXContext) 
        {
            this.httpContextAccessor = httpContextAccessor;
            this.vinylXContext = vinylXContext;
        }

        public async Task<User> GetLoggedInUser()
        {
            // Get AspNetUsersId from current logged-in user
            var aspNetUsersId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (aspNetUsersId == null)
            {
                // No user logged in
                throw new Exception("No user currently logged in");
            }

            // Lookup related User entity
            var user = await vinylXContext.User.SingleOrDefaultAsync(u => u.AspNetUsersId == aspNetUsersId);
            if (user != null)
            {
                return user;
            }

            // User entity did not exist -> Create it
            user = (await vinylXContext.User.AddAsync(new User { AspNetUsersId = aspNetUsersId })).Entity;
            await vinylXContext.SaveChangesAsync();

            return user;
        }
    }
}
