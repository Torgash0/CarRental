using DataAccess.Entities;
using DataAccess.Repository;

using FluentAssertions;

namespace DataAccess.UnitTests;

[TestFixture]
[Category("Integration")]
public class ScooterRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllScootersTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var scooters = new ScooterEntity[]
        {
            new()
            {
                Price = 1000.0,
                ChargePercentage = 80.0,
                Location = "Воронеж"
            },
            new()
            {
                Price = 800.0,
                ChargePercentage = 50.0,
                Location = "Москва"
            }
        };

        context.Scooters.AddRange(scooters);
        context.SaveChanges();

        var repository = new Repository<ScooterEntity>(_dbContextFactory);
        var actualScooters = repository.GetAll();

        actualScooters.Should().BeEquivalentTo(scooters, options => options.Excluding(scooters => scooters.Rents));
    }

    [Test]
    public void GetAllScootersWithFilterTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var scooters = new ScooterEntity[]
        {
            new()
            {
                Price = 1000.0,
                ChargePercentage = 80.0,
                Location = "Воронеж"
            },
            new()
            {
                Price = 800.0,
                ChargePercentage = 50.0,
                Location = "Москва"
            }
        };

        context.Scooters.AddRange(scooters);
        context.SaveChanges();

        var repository = new Repository<ScooterEntity>(_dbContextFactory);
        var actualScooters = repository.GetAll(scooters => scooters.Price == 1000.0).ToArray();

        actualScooters.Should().BeEquivalentTo(scooters.Where(scooters => scooters.Price == 1000.0));
    }

    [Test]
    public void SaveNewScooterTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var scooter = new ScooterEntity()
        {
            Price = 1000.0,
            ChargePercentage = 80.0,
            Location = "Воронеж"
        };

        var repository = new Repository<ScooterEntity>(_dbContextFactory);
        repository.Save(scooter);

        var actualScooter = context.Scooters.SingleOrDefault();

        if (actualScooter is null)
        {
            return;
        }

        actualScooter.Should().BeEquivalentTo(scooter, options => options.Excluding(scooter => scooter.Id)
                                                                         .Excluding(scooter => scooter.ExternalId)
                                                                         .Excluding(scooter => scooter.CreationTime)
                                                                         .Excluding(scooter => scooter.ModificationTime));
        actualScooter.Id.Should().NotBe(default);
        actualScooter.ExternalId.Should().NotBe(Guid.Empty);
        actualScooter.CreationTime.Should().NotBe(default);
        actualScooter.ModificationTime.Should().NotBe(default);
    }

    [Test]
    public void UpdateScooterTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var scooter = new ScooterEntity()
        {
            Price = 1000.0,
            ChargePercentage = 80.0,
            Location = "Воронеж"
        };

        scooter.Price = 200.0;
        scooter.ChargePercentage = 20.0;

        var repository = new Repository<ScooterEntity>(_dbContextFactory);
        repository.Save(scooter);

        var actualScooter = context.Scooters.SingleOrDefault();
        actualScooter.Should().BeEquivalentTo(scooter);
    }

    [Test]
    public void DeleteScooterTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var scooter = new ScooterEntity()
        {
            Price = 1000.0,
            ChargePercentage = 80.0,
            Location = "Воронеж"
        };

        context.Scooters.Add(scooter);
        context.SaveChanges();

        var repository = new Repository<ScooterEntity>(_dbContextFactory);
        repository.Delete(scooter);

        context.Scooters.Count().Should().Be(0);
    }

    [Test]
    public void GetByIdScooterTest()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var scooters = new ScooterEntity[]
        {
            new()
            {
                Price = 1000.0,
                ChargePercentage = 80.0,
                Location = "Воронеж"
            },
            new()
            {
                Price = 800.0,
                ChargePercentage = 50.0,
                Location = "Москва"
            }
        };

        context.Scooters.AddRange(scooters);
        context.SaveChanges();

        var repository = new Repository<ScooterEntity>(_dbContextFactory);
        var actualScooter = repository.GetById(scooters[0].Id);

        actualScooter.Should().BeEquivalentTo(scooters[0]);

        actualScooter = repository.GetById(scooters[^1].Id + 10);

        actualScooter.Should().BeNull();
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

        context.Scooters.RemoveRange(context.Scooters);
        context.SaveChanges();
    }
}
