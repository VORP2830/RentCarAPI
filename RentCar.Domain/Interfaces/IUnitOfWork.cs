namespace RentCar.Domain.Interfaces;

public interface IUnitOfWork
{
    ICarRepository CarRepository { get; }
    IClientRepository ClientRepository { get; }
    IRentRepository RentRepository { get; }
    Task<bool> SaveChangesAsync();
}
