using AutoMapper;

using BL.Cars.Entities;

using Service.Controllers.Entities.Cars;

namespace Service.Mapper;

public class CarsServiceProfile : Profile
{
    public CarsServiceProfile()
    {
        CreateMap<CarsFilter, CarsServiceProfile>();
        CreateMap<CreateCarRequest, CreateCarModel>();
        CreateMap<UpdateCarRequest, UpdateCarModel>();
    }
}
