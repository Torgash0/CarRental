using DataAccess.Entities;
using DataAccess.Repository;

using FluentAssertions;

namespace DataAccess.UnitTests;

[TestFixture]
[Category("Integration")]
public class UserRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllUsersTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var users = new UserEntity[]
        {
            new()
            {
                Name = "Name1",
                Surname = "Surname1",
                Email = "email1@email.com",
                PhoneNumber = "1234567890",
                PasswordHash = "PasswordHash1",
                IsAdmin = true
            },
            new()
            {
                Name = "Name2",
                Surname = "Surname2",
                Email = "email2@email.com",
                PhoneNumber = "0987654321",
                PasswordHash = "PasswordHash2",
                IsAdmin = false
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        var repository = new Repository<UserEntity>(_dbContextFactory);
        var actualUsers = repository.GetAll();

        actualUsers.Should().BeEquivalentTo(users);
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var users = new UserEntity[]
        {
            new()
            {
                Name = "Name1",
                Surname = "Surname1",
                Email = "email1@email.com",
                PhoneNumber = "1234567890",
                PasswordHash = "PasswordHash1",
                IsAdmin = true
            },
            new()
            {
                Name = "Name2",
                Surname = "Surname2",
                Email = "email2@email.com",
                PhoneNumber = "0987654321",
                PasswordHash = "PasswordHash2",
                IsAdmin = false
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        var repository = new Repository<UserEntity>(_dbContextFactory);
        var actualUsers = repository.GetAll(user => user.Email == "email1@email.com").ToArray();

        actualUsers.Should().BeEquivalentTo(users.Where(user => user.Email == "email1@email.com"));
    }

    [Test]
    public void SaveNewUserTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var user = new UserEntity()
        {
            Name = "Name1",
            Surname = "Surname1",
            Email = "email1@email.com",
            PhoneNumber = "1234567890",
            PasswordHash = "PasswordHash1",
            IsAdmin = true
        };

        var repository = new Repository<UserEntity>(_dbContextFactory);
        repository.Save(user);

        var actualUser = context.Users.SingleOrDefault();

        if (actualUser == null)
        {
            return;
        }

        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(user => user.Id)
                                                                   .Excluding(user => user.ExternalId)
                                                                   .Excluding(user => user.CreationTime)
                                                                   .Excluding(user => user.ModificationTime));
        actualUser.Id.Should().NotBe(default);
        actualUser.ExternalId.Should().NotBe(Guid.Empty);
        actualUser.CreationTime.Should().NotBe(default);
        actualUser.ModificationTime.Should().NotBe(default);
    }

    [Test]
    public void UpdateUserTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var user = new UserEntity()
        {
            Name = "Name1",
            Surname = "Surname1",
            Email = "email1@email.com",
            PhoneNumber = "1234567890",
            PasswordHash = "PasswordHash1",
            IsAdmin = true
        };

        context.Users.Add(user);
        context.SaveChanges();

        user.Name = "NewName";
        user.Surname = "NewSurname";

        var repository = new Repository<UserEntity>(_dbContextFactory);
        repository.Save(user);

        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user);
    }

    [Test]
    public void DeleteUserTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var user = new UserEntity()
        {
            Name = "Name1",
            Surname = "Surname1",
            Email = "email1@email.com",
            PhoneNumber = "1234567890",
            PasswordHash = "PasswordHash1",
            IsAdmin = true
        };

        context.Users.Add(user);
        context.SaveChanges();

        var repository = new Repository<UserEntity>(_dbContextFactory);
        repository.Delete(user);

        context.Users.Count().Should().Be(0);
    }

    [Test]
    public void GetByIdUserTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var users = new UserEntity[]
        {
            new()
            {
                Name = "Name1",
                Surname = "Surname1",
                Email = "email1@email.com",
                PhoneNumber = "1234567890",
                PasswordHash = "PasswordHash1",
                IsAdmin = true
            },
            new()
            {
                Name = "Name2",
                Surname = "Surname2",
                Email = "email2@email.com",
                PhoneNumber = "0987654321",
                PasswordHash = "PasswordHash2",
                IsAdmin = false
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        var repository = new Repository<UserEntity>(_dbContextFactory);
        var actualUser = repository.GetById(users[0].Id);

        actualUser.Should().BeEquivalentTo(users[0]);

        actualUser = repository.GetById(users[^1].Id + 10);

        actualUser.Should().BeNull();
    }

    [SetUp]
    public void SetUp()
    {
        CleanUp();
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();
    }

    private void CleanUp()
    {
        using var context = _dbContextFactory.CreateDbContext();

        context.Users.RemoveRange(context.Users);
        context.SaveChanges();
    }
}
