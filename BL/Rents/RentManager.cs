using AutoMapper;

using BL.Rents.Entities;

using DataAccess.Entities;
using DataAccess.Repository;

namespace BL.Rents;

public class RentManager(IRepository<RentEntity> repository, IMapper mapper) : IManager<RentModel, CreateRentModel>
{
    private readonly IRepository<RentEntity> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public RentModel Create(CreateRentModel model)
    {
        var entity = _mapper.Map<RentEntity>(model);

        _repository.Save(entity);

        return _mapper.Map<RentModel>(model);
    }

    public RentModel Update(Guid id, RentModel model)
    {
        var entity = Find(id);

        entity.User.ExternalId = model.UserId;
        entity.Cars = (ICollection<CarEntity>)model.Cars;
        entity.Start = model.Start;
        entity.End = model.End;
        entity.TotalPrice = model.TotalPrice;

        if (model.ReviewId != null)
        {
            if (entity.Review != null)
            {
                entity.Review.ExternalId = (Guid)model.ReviewId;
            }
        }

        _repository.Save(entity);

        return _mapper.Map<RentModel>(entity);
    }

    public void Delete(Guid id)
    {
        _repository.Delete(Find(id));
    }

    private RentEntity Find(Guid id)
    {
        return _repository.GetById(id) ?? throw new InvalidOperationException($"Rent with ID {id} not found.");
    }
}