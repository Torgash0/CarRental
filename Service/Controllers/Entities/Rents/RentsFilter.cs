namespace Service.Controllers.Entities.Rents;

public class RentsFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? MinTotalPrice { get; set; }
    public double? MaxTotalPrice { get; set; }
}
