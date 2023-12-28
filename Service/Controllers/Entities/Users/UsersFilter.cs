namespace Service.Controllers.Entities.Users;

public class UsersFilter
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int? MinRentCount { get; set; }
    public int? MaxRentCount { get; set; }
    public bool? IsAdmin { get; set; }
}
