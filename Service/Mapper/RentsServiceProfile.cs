using AutoMapper;

using BL.Rents.Entities;

using Service.Controllers.Entities.Rents;

namespace Service.Mapper;

public class RentsServiceProfile : Profile
{
    public RentsServiceProfile()
    {
        CreateMap<RentsFilter, RentsModelFilter>();
        CreateMap<CreateRentRequest, CreateRentModel>();
        CreateMap<UpdateRentRequest, UpdateRentModel>();
    }
}
