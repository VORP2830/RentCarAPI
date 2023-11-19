using RentCar.Domain.Entities;
using RentCar.Domain.Filter;

namespace RentCar.Domain.Interfaces;

public interface IRentRepository : IGenericRepository<Rent>
{
    Task<IEnumerable<Rent>> GetAll(RentFilter rentFilter);
    Task<Rent> GetById(Guid id);
}
