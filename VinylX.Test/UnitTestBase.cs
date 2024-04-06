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
            ArtistRepositoryMockup.Reset();
            services.AddScoped<IRepository<Folder>, FolderRepositoryMockup>();
            FolderRepositoryMockup.Reset();
            services.AddScoped<IRepository<MasterRelease>, MasterReleaseRepositoryMockup>();
            MasterReleaseRepositoryMockup.Reset();
            services.AddScoped<IRepository<Media>, MediaRepositoryMockup>();
            MediaRepositoryMockup.Reset();
            services.AddScoped<IRepository<RecordLabel>, RecordLabelRepositoryMockup>();
            RecordLabelRepositoryMockup.Reset();
            services.AddScoped<IRepository<ReleaseInstance>, ReleaseInstanceRepositoryMockup>();
            ReleaseInstanceRepositoryMockup.Reset();
            services.AddScoped<IRepository<Release>, ReleaseRepositoryMockup>();
            ReleaseRepositoryMockup.Reset();
            services.AddScoped<IRepository<User>, UserRepositoryMockup>();
            UserRepositoryMockup.Reset();
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
