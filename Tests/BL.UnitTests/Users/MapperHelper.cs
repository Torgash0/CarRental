using AutoMapper;

using Service.Mapper;

namespace BL.UnitTests.Users;

public static class MapperHelper
{
    public static IMapper Mapper { get; }

    static MapperHelper()
    {
        var config = new MapperConfiguration(x => x.AddProfile(typeof(UsersServiceProfile)));
        Mapper = new AutoMapper.Mapper(config);
    }
}
