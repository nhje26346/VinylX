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
            var repositoryFoundation = ServiceProvider.GetRequiredService<IRepositoryFoundation>();
            var userRepository = ServiceProvider.GetRequiredService<IRepository<User>>();
            var userService = (UserServiceMockup)ServiceProvider.GetRequiredService<IUserService>();

            var user = userRepository.Add(new User { AspNetUsersId = "123" });
            await repositoryFoundation.SaveChangesAsync();
            userService.SetLoggedInUser(user);

            var controller = GetController();

            await controller.Create(new Folder { FolderName = "Test", User = user });

            var folderRepository = ServiceProvider.GetRequiredService<IRepository<Folder>>();
            var folders = folderRepository.Queryable.Where(f => true);

            Assert.AreEqual(1, folders.Count(), "Unexpected number of folders");
            var folder = folders.Single();
            Assert.AreEqual("Test", folder.FolderName, "Unexpected folder name");
            Assert.AreEqual(user.UserId, folder.User.UserId);
        }

        private FoldersController GetController()
        {
            var repositoryFoundation = ServiceProvider.GetRequiredService<IRepositoryFoundation>();
            var folderRepository = ServiceProvider.GetRequiredService<IRepository<Folder>>();
            var userService = ServiceProvider.GetRequiredService<IUserService>();
            var releaseInstanceRepository = ServiceProvider.GetRequiredService<IRepository<ReleaseInstance>>();
            return new FoldersController(repositoryFoundation, folderRepository, userService, releaseInstanceRepository);
        }
    }
}