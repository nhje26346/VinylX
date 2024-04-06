using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Models;
using VinylX.Repositories;

namespace VinylX.Test
{
    [TestClass]
    public class UnitTestRepositoryMockups : UnitTestBase
    {
        [TestMethod]
        public async Task TestAddUser()
        {
            var userRepository = ServiceProvider.GetRequiredService<IRepository<User>>();
            var user = userRepository.Add(new User { AspNetUsersId = "..." });
            Assert.AreEqual(0, user.UserId, $"User was not expected to have an ID before saving!");
            var repositoryFoundation = ServiceProvider.GetRequiredService<IRepositoryFoundation>();
            await repositoryFoundation.SaveChangesAsync();
            Assert.IsTrue(user.UserId > 0, "User was expected to have an ID after saving!");
        }
    }
}
