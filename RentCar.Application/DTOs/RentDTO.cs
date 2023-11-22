namespace RentCar.Application.DTOs;

public class RentDTO
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CarId { get; set; }
    public DateTime RentDate { get; set; }
    public string Operation { get; set; }
}
