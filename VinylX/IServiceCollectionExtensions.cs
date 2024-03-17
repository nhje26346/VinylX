using VinylX.Services;
using VinylX.Services.Implementations;

namespace VinylX
{
    internal static class IServiceCollectionExtensions
    {
        public static void AddVinylXServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
