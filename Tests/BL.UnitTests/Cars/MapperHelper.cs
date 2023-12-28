using AutoMapper;

using Service.Mapper;

namespace BL.UnitTests.Cars;

public static class MapperHelper
{
    public static IMapper Mapper { get; }

    static MapperHelper()
    {
        var config = new MapperConfiguration(x => x.AddProfile(typeof(CarsServiceProfile)));
        Mapper = new AutoMapper.Mapper(config);
    }
}
