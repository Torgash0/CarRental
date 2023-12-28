using System.Net;

using DataAccess.Entities;
using DataAccess.Repository;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Service.UnitTests.Users.Authorization;

public class LoginUserTests : ScooterRentalServiceTestsBaseClass
{
    [Test]
    public async Task NotFoundUserNotFoundResultTest()
    {
        var login = "not_existing@mail.ru";
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var user = userRepository.GetAll().FirstOrDefault(x => x.UserName.ToLower() == login.ToLower());

        if (user != null)
        {
            userRepository.Delete(user);
        }

        var password = "password";
        var query = $"?email={login}&password={password}";
        var requestUri = ScooterRentalApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new(HttpMethod.Get, requestUri);
        var response = await TestHttpClient.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task PasswordIsIncorrectResultTest()
    {
        var user = new UserEntity()
        {
            Email = "test@test.com",
            UserName = "testtest",
            Name = "test",
            Surname = "test",
            IsAdmin = false
        };
        var password = "password";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
        await userManager.CreateAsync(user, password);

        var incorrect_password = "kvhdbkvhbk";

        var query = $"?email={user.UserName}&password={incorrect_password}";
        var requestUri = ScooterRentalApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    [TestCase("", "")]
    [TestCase("qwe", "")]
    [TestCase("test@test", "")]
    [TestCase("", "password")]
    public async Task LoginOrPasswordAreInvalidResultTest(string login, string password)
    {
        var query = $"?login={login}&password={password}";
        var requestUri = ScooterRentalApiEndpoints.AuthorizeUserEndpoint + query;

        HttpRequestMessage request = new(HttpMethod.Get, requestUri);

        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
