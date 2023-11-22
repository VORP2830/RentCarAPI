using AutoMapper;
using RentCar.Application.DTOs;
using RentCar.Application.Interfaces;
using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;

namespace RentCar.Application.Services;

public class ClientService : IClientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClientDTO>> GetAll(ClientFilter clientFilter)
    {
        IEnumerable<Client> clients = await _unitOfWork.ClientRepository.GetAll(clientFilter);
        return _mapper.Map<IEnumerable<ClientDTO>>(clients);
    }

    public async Task<ClientDTO> GetById(Guid id)
    {
        Client client = await _unitOfWork.ClientRepository.GetById(id);
        return _mapper.Map<ClientDTO>(client);
    }

    public async Task<ClientDTO> Create(ClientDTO model)
    {
        Client client = _mapper.Map<Client>(model);
        await CheckCPF(model.CPF);
        _unitOfWork.ClientRepository.Add(client);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ClientDTO>(client);
    }

    public async Task<ClientDTO> Update(ClientDTO model)
    {
        Client client = await _unitOfWork.ClientRepository.GetById(model.Id);
        if(client is null) throw new RentCarException("Cliente não encontrado");
        await CheckCPF(model.CPF);
        _mapper.Map(client, model);
        _unitOfWork.ClientRepository.Update(client);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ClientDTO>(client);
    }

    public async Task<ClientDTO> Delete(Guid id)
    {
        Client client = await _unitOfWork.ClientRepository.GetById(id);
        if(client is null) throw new RentCarException("Cliente não encontrado");
        client.SetActive(false);
        _unitOfWork.ClientRepository.Update(client);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ClientDTO>(client);
    }

    private async Task CheckCPF(string cpf)
    {
        ClientFilter clientFilter = new ClientFilter
        {
            CPF = cpf
        };
        IEnumerable<Client> clients = await _unitOfWork.ClientRepository.GetAll(clientFilter);
        if(clients.Count() > 0) throw new RentCarException("CPF já existe na base de dados");
    }
}
