using System.Linq.Expressions;

using BL.Rents;

using DataAccess.Entities;
using DataAccess.Repository;

using Moq;

namespace BL.UnitTests.Rents;

[TestFixture]
public class RentProviderTests
{
    [Test]
    public void TestGetAllRents()
    {
        Expression? expression = null;
        Mock<IRepository<RentEntity>> rentsRepository = new();

        rentsRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<RentEntity, bool>>>()))
            .Callback((Expression<Func<RentEntity, bool>> x) => { expression = x; });

        var rentsProvider = new RentProvider(rentsRepository.Object, MapperHelper.Mapper);
        var rents = rentsProvider.Get();

        rentsRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<RentEntity, bool>>>()), Times.Exactly(1));
    }
}
