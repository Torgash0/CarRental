using System.Linq.Expressions;

using BL.Cars;

using DataAccess.Entities;
using DataAccess.Repository;

using Moq;

namespace BL.UnitTests.Cars;

[TestFixture]
public class CarProviderTests
{
    [Test]
    public void TestGetAllCars()
    {
        Expression? expression = null;
        Mock<IRepository<CarEntity>> CarsRepository = new();

        CarsRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<CarEntity, bool>>>()))
            .Callback((Expression<Func<CarEntity, bool>> x) => { expression = x; });

        var CarsProvider = new CarProvider(CarsRepository.Object, MapperHelper.Mapper);
        var Cars = CarsProvider.Get();

        CarsRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<CarEntity, bool>>>()), Times.Exactly(1));
    }
}
