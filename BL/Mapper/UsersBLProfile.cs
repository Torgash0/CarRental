using AutoMapper;

using BL.Users.Entities;

using DataAccess.Entities;

namespace BL.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(user => user.Id, x => x.MapFrom(src => src.ExternalId))
            .ForMember(user => user.Name, x => x.MapFrom(src => src.Name))
            .ForMember(user => user.Surname, x => x.MapFrom(src => src.Surname))
            .ForMember(user => user.PhoneNumber, x => x.MapFrom(src => src.PhoneNumber))
            .ForMember(user => user.IsAdmin, x => x.MapFrom(src => src.IsAdmin));

        CreateMap<CreateUserModel, UserEntity>()
            .ForMember(user => user.Id, x => x.Ignore())
            .ForMember(user => user.ExternalId, x => x.Ignore())
            .ForMember(user => user.CreationTime, x => x.Ignore())
            .ForMember(user => user.ModificationTime, x => x.Ignore());
    }
}
