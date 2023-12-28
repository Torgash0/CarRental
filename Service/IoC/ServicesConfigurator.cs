using AutoMapper;

using BL;
using BL.Auth;
using BL.Rents;
using BL.Rents.Entities;
using BL.Cars;
using BL.Cars.Entities;
using BL.Users;
using BL.Users.Entities;

using DataAccess.Entities;
using DataAccess.Repository;

using Microsoft.AspNetCore.Identity;

using Service.Settings;

namespace Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureService(IServiceCollection services, CarRentalSettings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IProvider<UserModel, UsersModelFilter>>(x => new UserProvider(x.GetRequiredService<IRepository<UserEntity>>(), x.GetRequiredService<IMapper>()));

        services.AddScoped<IAuthProvider>(x =>
                new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                x.GetRequiredService<UserManager<UserEntity>>(),
                x.GetRequiredService<IHttpClientFactory>(),
                settings.IdentityServerUri,
                settings.ClientId,
                settings.ClientSecret));

        services.AddScoped<IManager<RentModel, CreateRentModel>, RentManager>();
        services.AddScoped<IManager<CarModel, CreateCarModel>, CarManager>();
        services.AddScoped<IManager<UserModel, CreateUserModel>, UserManager>();
    }
}
