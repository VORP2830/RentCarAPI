using RentCar.Application.DTOs;
using RentCar.Domain.Filter;

namespace RentCar.Application.Interfaces;

public interface ICarService
{
    Task<IEnumerable<CarDTO>> GetAll(CarFilter carFilter);
    Task<CarDTO> GetById(Guid id);
    Task<CarDTO> Create(CarDTO model);
    Task<CarDTO>  Update(CarDTO model);
    Task<CarDTO>  Delete(Guid id);
}