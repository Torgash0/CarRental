namespace BL.Users.Entities;

public class UpdateUserModel
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public bool? IsAdmin { get; set; }
}
