namespace BL.Users.Entities;

public class UsersModelFilter
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int? MinRentCount { get; set; }
    public int? MaxRentCount { get; set; }
    public bool? IsAdmin { get; set; }
}
