using AutoMapper;

using BL.Cars.Entities;

using DataAccess.Entities;
using DataAccess.Repository;

namespace BL.Cars;

public class CarProvider(IRepository<CarEntity> CarsRepository, IMapper mapper) : IProvider<CarModel, CarsModelFilter>
{
    private readonly IRepository<CarEntity> _repository = CarsRepository;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<CarModel> Get(CarsModelFilter? modelFilter = null)
    {
        double? minPrice = modelFilter?.MinPrice;
        double? maxPrice = modelFilter?.MaxPrice;
        double? minChargePercentage = modelFilter?.MinChargePercentage;
        double? maxChargePercentage = modelFilter?.MaxChargePercentage;
        string? location = modelFilter?.Location;

        var Cars = _repository.GetAll(Car => (
        (minPrice == null || Car.Price >= minPrice) &&
        (maxPrice == null || Car.Price <= maxPrice) &&
        (minChargePercentage == null || Car.ChargePercentage >= minPrice) &&
        (maxChargePercentage == null || Car.ChargePercentage <= maxChargePercentage) &&
        (location == null || Car.Location == location)
        ));

        return _mapper.Map<IEnumerable<CarModel>>(Cars);
    }

    public CarModel GetInfo(Guid id)
    {
        return _mapper.Map<CarModel>(Find(id));
    }

    private CarEntity Find(Guid id)
    {
        return _repository.GetById(id) ?? throw new InvalidOperationException($"Car with ID {id} not found.");
    }
}
