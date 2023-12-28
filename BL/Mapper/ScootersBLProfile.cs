using AutoMapper;

using BL.Scooters.Entities;

using DataAccess.Entities;

namespace BL.Mapper;

public class ScootersBLProfile : Profile
{
    public ScootersBLProfile()
    {
        CreateMap<ScooterEntity, ScooterModel>()
            .ForMember(scooter => scooter.Id, x => x.MapFrom(src => src.ExternalId))
            .ForMember(scooter => scooter.Price, x => x.MapFrom(src => src.Price))
            .ForMember(scooter => scooter.ChargePercentage, x => x.MapFrom(src => src.ChargePercentage))
            .ForMember(scooter => scooter.Location, x => x.MapFrom(src => src.Location))
            .ForMember(scooter => scooter.Rents, x => x.MapFrom(src => src.Rents));

        CreateMap<CreateScooterModel, ScooterEntity>()
            .ForMember(scooter => scooter.Id, x => x.Ignore())
            .ForMember(scooter => scooter.ExternalId, x => x.Ignore())
            .ForMember(scooter => scooter.CreationTime, x => x.Ignore())
            .ForMember(scooter => scooter.ModificationTime, x => x.Ignore());
    }
}
