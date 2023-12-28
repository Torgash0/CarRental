using BL.Users.Entities;

namespace Service.Controllers.Entities.Users;

public class UsersListResponse
{
    public List<UserModel>? Users { get; set; }
}
