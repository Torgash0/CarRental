using AutoMapper;

using BL.Rents.Entities;

using DataAccess.Entities;
using DataAccess.Repository;

namespace BL.Rents;

public class RentProvider(IRepository<RentEntity> repository, IMapper mapper) : IProvider<RentModel, RentsModelFilter>
{
    private readonly IRepository<RentEntity> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<RentModel> Get(RentsModelFilter? modelFilter = null)
    {
        DateTime? startDate = modelFilter?.StartDate;
        DateTime? endDate = modelFilter?.EndDate;
        double? minTotalPrice = modelFilter?.MinTotalPrice;
        double? maxTotalPrice = modelFilter?.MaxTotalPrice;

        var rents = _repository.GetAll(rent => (
        (startDate == null || rent.Start >= startDate) &&
        (endDate == null || rent.End <= endDate) &&
        (minTotalPrice == null || rent.TotalPrice >= minTotalPrice) &&
        (maxTotalPrice == null || rent.TotalPrice <= maxTotalPrice)
        ));

        return _mapper.Map<IEnumerable<RentModel>>(rents);
    }

    public RentModel GetInfo(Guid id)
    {
        return _mapper.Map<RentModel>(Find(id));
    }

    private RentEntity Find(Guid id)
    {
        return _repository.GetById(id) ?? throw new InvalidOperationException($"Rent with ID {id} not found.");
    }
}
