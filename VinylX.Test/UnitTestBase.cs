using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX;
using VinylX.Models;
using VinylX.Repositories;
using VinylX.Services;
using VinylX.Test.Mockups.Repositories;
using VinylX.Test.Mockups.Services;

namespace VinylX.Test
{
    public class UnitTestBase
    {
        private IServiceProvider? serviceProvider;
        protected IServiceProvider ServiceProvider => serviceProvider ?? throw new Exception($"{nameof(IServiceProvider)} not initialized!");

        [TestInitialize]
        public void Initialize()
        {
            var serviceCollection = new ServiceCollection() as IServiceCollection;
            serviceCollection.AddVinylXServices();
            AddRepositoryMockups(serviceCollection);
            AddServiceMockups(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void AddServiceMockups(IServiceCollection services) 
        {
            services.AddScoped<IUserService, UserServiceMockup>();
        }
        
        private void AddRepositoryMockups(IServiceCollection services) 
        {
            services.AddScoped<IRepositoryFoundation, RepositoryFoundationMockup>();
            services.AddScoped<IRepository<Artist>, ArtistRepositoryMockup>();
            services.AddScoped<IRepository<Folder>, FolderRepositoryMockup>();
            services.AddScoped<IRepository<MasterRelease>, MasterReleaseRepositoryMockup>();
            services.AddScoped<IRepository<Media>, MediaRepositoryMockup>();
            services.AddScoped<IRepository<RecordLabel>, RecordLabelRepositoryMockup>();
            services.AddScoped<IRepository<ReleaseInstance>, ReleaseInstanceRepositoryMockup>();
            services.AddScoped<IRepository<Release>, ReleaseRepositoryMockup>();
            services.AddScoped<IRepository<User>, UserRepositoryMockup>();
        }

        protected async Task<User> CreateUser(User user, bool setAsLoggedIn)
        {
            var newUser = GetRepository<User>().Add(user);
            await Save();

            if (setAsLoggedIn)
            {
                var userService = (UserServiceMockup)ServiceProvider.GetRequiredService<IUserService>();
                userService.SetLoggedInUser(newUser);
            }

            return newUser;
        }

        protected IRepository<TEntity> GetRepository<TEntity>() where TEntity : class => ServiceProvider.GetRequiredService<IRepository<TEntity>>();

        protected Task Save() => ServiceProvider.GetRequiredService<IRepositoryFoundation>().SaveChangesAsync();
    }
}
