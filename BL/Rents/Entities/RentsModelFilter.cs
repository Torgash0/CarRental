namespace BL.Rents.Entities;

public class RentsModelFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? MinTotalPrice { get; set; }
    public double? MaxTotalPrice { get; set; }
}
