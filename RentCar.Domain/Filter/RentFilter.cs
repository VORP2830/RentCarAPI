namespace RentCar.Domain.Filter;

public class RentFilter
{
    public Guid ClientId { get; set; }
    public Guid CarId { get; set; }
    public DateTime RentDate { get; set; }
    public string Operation { get; set; } = string.Empty;
}
