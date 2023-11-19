using RentCar.Domain.Entities;
using RentCar.Domain.Filter;

namespace RentCar.Domain.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<IEnumerable<Client>> GetAll(ClientFilter clientFilter);
    Task<Client> GetById(Guid id);
}