using AutoMapper;
using RentCar.Application.DTOs;
using RentCar.Application.Interfaces;
using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;

namespace RentCar.Application.Services;

public class RentService : IRentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RentDTO>> GetAll(RentFilter rentFilter)
    {
        IEnumerable<Rent> rents = await _unitOfWork.RentRepository.GetAll(rentFilter);
        return _mapper.Map<IEnumerable<RentDTO>>(rents);
    }

    public async Task<RentDTO> GetById(Guid id)
    {
        Rent rent = await _unitOfWork.RentRepository.GetById(id);
        return _mapper.Map<RentDTO>(rent);
    }

    public async Task<RentDTO> Create(RentDTO model)
    {
        Client client = await _unitOfWork.ClientRepository.GetById(model.ClientId);
        if(client is null) throw new RentCarException("Cliente não encontrado");
        bool isAvailable = await isAvailableCar(model.CarId);
        if(!isAvailable) throw new RentCarException("Carro não disponível");
        Rent rent = _mapper.Map<Rent>(model);
        _unitOfWork.RentRepository.Add(rent);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RentDTO>(rent);
    }

    public async Task<RentDTO> Update(RentDTO model)
    {
        Rent rent = await _unitOfWork.RentRepository.GetById(model.Id);
        if(rent is null) throw new RentCarException("Aluguel não encontrado");
        Client client = await _unitOfWork.ClientRepository.GetById(model.ClientId);
        if(client is null) throw new RentCarException("Cliente não encontrado");
        bool isAvailable = await isAvailableCar(model.CarId);
        if(!isAvailable) throw new RentCarException("Carro não disponível");
        _mapper.Map(rent, model);
        _unitOfWork.RentRepository.Update(rent);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RentDTO>(rent);
    }

    public async Task<RentDTO> Delete(Guid id)
    {
        Rent rent = await _unitOfWork.RentRepository.GetById(id);
        if(rent is null) throw new RentCarException("Aluguel não encontrado");
        rent.SetActive(false);
        _unitOfWork.RentRepository.Update(rent);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<RentDTO>(rent);
    }

    private async Task<bool> isAvailableCar(Guid carId)
    {
        Car car = await _unitOfWork.CarRepository.GetById(carId);
        if(car is null) throw new RentCarException("Carro não encontrado");
        RentFilter rentFilter = new RentFilter{ CarId = carId };
        IEnumerable<Rent> rents = await _unitOfWork.RentRepository.GetAll(rentFilter);
        if(rents is null) return true;
        Rent lastRent = rents.LastOrDefault();
        if(lastRent.Operation == "D") return true;
        return false;
    }
}
