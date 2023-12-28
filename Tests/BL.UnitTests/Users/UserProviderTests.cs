using System.Linq.Expressions;

using BL.Users;

using DataAccess.Entities;
using DataAccess.Repository;

using Moq;

namespace BL.UnitTests.Users;

[TestFixture]
public class CarProviderTests
{
    [Test]
    public void TestGetAllUsers()
    {
        Expression? expression = null;
        Mock<IRepository<UserEntity>> usersRepository = new();

        usersRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
            .Callback((Expression<Func<UserEntity, bool>> x) => { expression = x; });

        var usersProvider = new UserProvider(usersRepository.Object, MapperHelper.Mapper);
        var users = usersProvider.Get();

        usersRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()), Times.Exactly(1));
    }
}
