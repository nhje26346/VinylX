using VinylX.Models;
using VinylX.Repositories;
using VinylX.Repositories.Implementations;
using VinylX.Services;
using VinylX.Services.Implementations;

namespace VinylX
{
    public static class IServiceCollectionExtensions
    {
        public static void AddVinylXServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddVinylXRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryFoundation, RepositoryFoundation>();
            services.AddScoped<IRepository<Artist>, ArtistRepository>();
            services.AddScoped<IRepository<Folder>, FolderRepository>();
            services.AddScoped<IRepository<MasterRelease>, MasterReleaseRepository>();
            services.AddScoped<IRepository<Media>, MediaRepository>();
            services.AddScoped<IRepository<RecordLabel>, RecordLabelRepository>();
            services.AddScoped<IRepository<ReleaseInstance>, ReleaseInstanceRepository>();
            services.AddScoped<IRepository<Release>, ReleaseRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
        }
    }
}
