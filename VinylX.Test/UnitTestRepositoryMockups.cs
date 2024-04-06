using Microsoft.EntityFrameworkCore;
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
            var user = GetRepository<User>().Add(new User { AspNetUsersId = "..." });
            Assert.AreEqual(0, user.UserId, $"User was not expected to have an ID before saving!");
            await Save();
            Assert.IsTrue(user.UserId > 0, "User was expected to have an ID after saving!");
            var users = GetRepository<User>().Queryable.Where(u => true).ToList();
            Assert.AreEqual(1, users.Count, "Invalid number of users!");
        }

        [TestMethod]
        public async Task TestIncludes()
        {
            // ARRANGE
            var user = GetRepository<User>().Add(new User { AspNetUsersId = "..." });
            await Save();
            var folder = GetRepository<Folder>().Add(new Folder { FolderName = "123", User = user });
            await Save();

            // ACT
            var foldersWithUser = GetRepository<Folder>().Queryable.Include(f => f.User).Where(f => true).ToList();
            var foldersWithoutUser = GetRepository<Folder>().Queryable.Where(f => true).ToList();

            // ASSERT
            Assert.AreEqual(1, foldersWithUser.Count);
            Assert.AreEqual(1, foldersWithoutUser.Count);
        }
    }
}
