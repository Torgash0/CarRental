using AutoMapper;

using BL.Users.Entities;

using Service.Controllers.Entities.Users;

namespace Service.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UsersFilter, UsersModelFilter>();
        CreateMap<CreateUserRequest, CreateUserModel>();
        CreateMap<UpdateUserRequest, UpdateUserModel>();
    }
}
