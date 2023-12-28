using AutoMapper;

using BL.Users.Entities;

using DataAccess.Entities;
using DataAccess.Repository;

namespace BL.Users;

public class UserManager(IRepository<UserEntity> usersRepository, IMapper mapper) : IManager<UserModel, CreateUserModel>
{
    private readonly IRepository<UserEntity> _usersRepository = usersRepository;
    private readonly IMapper _mapper = mapper;

    public UserModel Create(CreateUserModel model)
    {
        var entity = _mapper.Map<UserEntity>(model);

        _usersRepository.Save(entity);

        return _mapper.Map<UserModel>(entity);
    }

    public UserModel Update(Guid id, UserModel model)
    {
        var user = Find(id);

        user.Name = model.Name;
        user.Surname = model.Surname;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.Rents = (ICollection<RentEntity>?)model.Rents;
        user.IsAdmin = model.IsAdmin;

        _usersRepository.Save(user);

        return _mapper.Map<UserModel>(user);
    }

    public void Delete(Guid id)
    {
        _usersRepository.Delete(Find(id));
    }

    private UserEntity Find(Guid id)
    {
        return _usersRepository.GetById(id) ?? throw new InvalidOperationException($"User with ID {id} not found.");
    }
}
