using AutoMapper;

using BL.Rents.Entities;

using DataAccess.Entities;

namespace BL.Mapper;

public class RentsBLProfile : Profile
{
    public RentsBLProfile()
    {
        CreateMap<RentEntity, RentModel>()
            .ForMember(rent => rent.Id, x => x.MapFrom(src => src.ExternalId))
            .ForMember(rent => rent.UserId, x => x.MapFrom(src => src.User.ExternalId))
            .ForMember(rent => rent.Scooters, x => x.MapFrom(src => src.Scooters))
            .ForMember(rent => rent.Start, x => x.MapFrom(src => src.Start))
            .ForMember(rent => rent.End, x => x.MapFrom(src => src.End))
            .ForMember(rent => rent.TotalPrice, x => x.MapFrom(src => src.TotalPrice))
            .ForMember(rent => rent.ReviewId, x => x.MapFrom(src => src.Review.ExternalId));

        CreateMap<CreateRentModel, RentEntity>()
            .ForMember(rent => rent.Id, x => x.Ignore())
            .ForMember(rent => rent.ExternalId, x => x.Ignore())
            .ForMember(rent => rent.CreationTime, x => x.Ignore())
            .ForMember(rent => rent.ModificationTime, x => x.Ignore());
    }
}
