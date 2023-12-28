using AutoMapper;

using BL.Cars.Entities;

using DataAccess.Entities;
using DataAccess.Repository;

namespace BL.Cars;

public class CarManager(IRepository<CarEntity> repository, IMapper mapper) : IManager<CarModel, CreateCarModel>
{
    private readonly IRepository<CarEntity> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public CarModel Create(CreateCarModel model)
    {
        var entity = _mapper.Map<CarEntity>(model);

        _repository.Save(entity);

        return _mapper.Map<CarModel>(entity);
    }

    public CarModel Update(Guid id, CarModel model)
    {
        var Car = Find(id);

        Car.Price = model.Price;
        Car.ChargePercentage = model.ChargePercentage;
        Car.Location = model.Location;
        Car.Rents = (ICollection<RentEntity>?)model.Rents;

        _repository.Save(Car);

        return _mapper.Map<CarModel>(Car);
    }

    public void Delete(Guid id)
    {
        _repository.Delete(Find(id));
    }

    private CarEntity Find(Guid id)
    {
        return _repository.GetById(id) ?? throw new InvalidOperationException($"Car with ID {id} not found.");
    }
}
