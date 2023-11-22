namespace RentCar.Application.DTOs;

public class ClientDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public DateOnly BirthDay { get; set; }
}
