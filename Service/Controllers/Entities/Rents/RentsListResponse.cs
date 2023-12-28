using BL.Rents.Entities;

namespace Service.Controllers.Entities.Rents;

public class RentsListResponse
{
    public List<RentModel>? Rents { get; set; }
}
