using RentCar.Application.DTOs;
using RentCar.Domain.Filter;

namespace RentCar.Application.Interfaces;

public interface IRentService
{
    Task<IEnumerable<RentDTO>> GetAll(RentFilter rentFilter);
    Task<RentDTO> GetById(Guid id);
    Task<RentDTO> Create(RentDTO model);
    Task<RentDTO> Update(RentDTO model);
    Task<RentDTO> Delete(Guid id);
}
