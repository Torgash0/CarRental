using BL.Rents.Entities;

namespace BL.Users.Entities;

public class UserModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public List<RentModel>? Rents { get; set; }
    public required bool IsAdmin { get; set; }
}
