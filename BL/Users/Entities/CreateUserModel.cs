namespace BL.Users.Entities;

public class CreateUserModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
    public required bool IsAdmin { get; set; }
}
