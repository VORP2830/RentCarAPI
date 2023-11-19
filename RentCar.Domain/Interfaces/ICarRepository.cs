using RentCar.Domain.Entities;
using RentCar.Domain.Filter;

namespace RentCar.Domain.Interfaces;

public interface ICarRepository : IGenericRepository<Car>
{
    Task<IEnumerable<Car>> GetAll(CarFilter carFilter);
    Task<Car> GetById(Guid id);
}
