using AutoMapper;

using BL.Users.Entities;

using DataAccess.Entities;
using DataAccess.Repository;

namespace BL.Users;

public class UserProvider(IRepository<UserEntity> usersRepository, IMapper mapper) : IProvider<UserModel, UsersModelFilter>
{
    private readonly IRepository<UserEntity> _repository = usersRepository;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<UserModel> Get(UsersModelFilter? modelFilter = null)
    {
        string? name = modelFilter?.Name;
        string? surname = modelFilter?.Surname;
        int? minRentCount = modelFilter?.MinRentCount;
        int? maxRentCount = modelFilter?.MaxRentCount;
        bool? isAdmin = modelFilter?.IsAdmin;

        var users = _repository.GetAll(user => (
        (name == null || user.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
        (surname == null || user.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase)) &&
        (minRentCount == null || user.Rents != null && user.Rents.Count >= minRentCount) &&
        (maxRentCount == null || user.Rents != null && user.Rents.Count <= maxRentCount) &&
        (isAdmin == null || user.IsAdmin)
        ));

        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GetInfo(Guid id)
    {
        return _mapper.Map<UserModel>(Find(id));
    }

    private UserEntity Find(Guid id)
    {
        return _repository.GetById(id) ?? throw new InvalidOperationException($"User with ID {id} not found.");
    }
}
