using Microsoft.Extensions.DependencyInjection;
using VinylX.Controllers;
using VinylX.Models;
using VinylX.Repositories;
using VinylX.Services;
using VinylX.Test.Mockups.Services;

namespace VinylX.Test
{
    [TestClass]
    public class UnitTestFolderController : UnitTestBase
    {
        [TestMethod]
        public async Task CreateNewFolder()
        {
            // ARRANGE
            var user = await CreateUser(new User { AspNetUsersId = "123" }, true);
            var controller = GetController();

            // ACT
            await controller.Create(new Folder { FolderName = "Test", User = user });

            // ASSERT
            var folders = GetRepository<Folder>().Queryable.Where(f => true);
            Assert.AreEqual(1, folders.Count(), "Unexpected number of folders");
            var folder = folders.Single();
            Assert.AreEqual("Test", folder.FolderName, "Unexpected folder name");
            Assert.AreEqual(user.UserId, folder.User.UserId);
        }

        private FoldersController GetController()
        {
            var repositoryFoundation = ServiceProvider.GetRequiredService<IRepositoryFoundation>();
            var folderRepository = GetRepository<Folder>();
            var userService = ServiceProvider.GetRequiredService<IUserService>();
            var releaseInstanceRepository = GetRepository<ReleaseInstance>();
            return new FoldersController(repositoryFoundation, folderRepository, userService, releaseInstanceRepository);
        }
    }
}