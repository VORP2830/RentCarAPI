using RentCar.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace RentCar.Infra.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICarRepository CarRepository => new CarRepository(_context);

    public IClientRepository ClientRepository => new ClientRepository(_context);

    public IRentRepository RentRepository => new RentRepository(_context);

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
