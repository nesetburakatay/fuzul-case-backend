using Core.Repositories;
using Core.Services;
using Repository.Repositories;
using Service.Services;

namespace API
{
    public static class MyConfigurations
    {
        public static IServiceCollection MyInjections(this IServiceCollection services)
        {
            //services injections
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEstateService, EstateService>();
            services.AddScoped<ICountryService, CountryService>();


            //repository injections
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEstateRepository, EstateRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();


            //Background Services

            return services;
        }
    }
}
