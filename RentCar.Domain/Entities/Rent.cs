using RentCar.Domain.Exceptions;

namespace RentCar.Domain.Entities;

public class Rent : BaseEntity
{
    public Guid ClientId { get; protected set; }
    public Guid CarId { get; protected set; }
    public DateTime RentDate { get; protected set; }
    public string Operation { get; protected set; }
    public Car Car { get; protected set; }
    public Client Client { get; protected set; }
    protected Rent() { }
    public Rent(Guid clientId, Guid carId, DateTime rentDate, string operation)
    {
        ValidateDomain(clientId, carId, rentDate, operation);
        Active = true;
    }
    private void ValidateDomain(Guid clientId, Guid carId, DateTime rentDate, string operation)
    {
        RentCarException.When(rentDate < DateTime.Now.AddHours(-1), "A operação deve ser realizada dentro de 1 hora após a locação");
        RentCarException.When(rentDate > DateTime.Now.AddHours(1), "A operação deve ser agendada com no máximo 1 hora de antecedência");
        RentCarException.When(clientId == Guid.Empty, "ClientId não pode ser vazio");
        RentCarException.When(carId == Guid.Empty, "CarId não pode ser vazio");
        RentCarException.When(ClientId == null, "Client não pode ser nulo");
        RentCarException.When(CarId == null, "Car não pode ser nulo");
        RentCarException.When(
            !string.Equals(operation, "A", StringComparison.OrdinalIgnoreCase) && 
            !string.Equals(operation, "D", StringComparison.OrdinalIgnoreCase),
            "A operação só pode ser 'A' para aluguel e 'D' para devolução"
        );
        ClientId = clientId;
        CarId = carId;
        RentDate = rentDate;
        Operation = operation;
    }
}