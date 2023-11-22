using RentCar.Application.DTOs;
using RentCar.Domain.Filter;

namespace RentCar.Application.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientDTO>> GetAll(ClientFilter clientFilter);
    Task<ClientDTO> GetById(Guid id);
    Task<ClientDTO> Create(ClientDTO model);
    Task<ClientDTO>  Update(ClientDTO model);
    Task<ClientDTO>  Delete(Guid id);
}
