using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Services;

namespace VinylX.Test.Mockups.Services
{
    internal class UserServiceMockup : IUserService
    {
        protected User? user;

        public Task<User> GetLoggedInUser() => Task.FromResult(user ?? throw new Exception("No user found!"));

        public void SetLoggedInUser(User user)
        {
            this.user = user;
        }
    }
}
