using AutoMapper;

using Service.Mapper;

namespace BL.UnitTests.Rents;

public static class MapperHelper
{
    public static IMapper Mapper { get; }

    static MapperHelper()
    {
        var config = new MapperConfiguration(x => x.AddProfile(typeof(RentsServiceProfile)));
        Mapper = new AutoMapper.Mapper(config);
    }
}
