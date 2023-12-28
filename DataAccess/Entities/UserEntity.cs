using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities;

[Table("users")]
public class UserEntity : IdentityUser<int>, IBaseEntity
{
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }

    public required string Name { get; set; }
    public required string Surname { get; set; }
    public virtual ICollection<RentEntity>? Rents { get; set; }
    public required bool IsAdmin { get; set; }
}

public class UserRoleEntity : IdentityRole<int> { }