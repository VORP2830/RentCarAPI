using AutoMapper;
using RentCar.Application.DTOs;
using RentCar.Application.Interfaces;
using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;

namespace RentCar.Application.Services;

public class CarService : ICarService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CarService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CarDTO>> GetAll(CarFilter carFilter)
    {
        IEnumerable<Car> cars = await _unitOfWork.CarRepository.GetAll(carFilter);
        return _mapper.Map<IEnumerable<CarDTO>>(cars);
    }

    public async Task<CarDTO> GetById(Guid id)
    {
        Car car = await _unitOfWork.CarRepository.GetById(id);
        return _mapper.Map<CarDTO>(car);
    }

    public async Task<CarDTO> Create(CarDTO model)
    {
        Car car = _mapper.Map<Car>(model);
        _unitOfWork.CarRepository.Add(car);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CarDTO>(car);
    }

    public async Task<CarDTO> Update(CarDTO model)
    {
        Car car = await _unitOfWork.CarRepository.GetById(model.Id);
        if(car is null) throw new RentCarException("Carro não encontrado");
        _mapper.Map(car, model);
        _unitOfWork.CarRepository.Update(car);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CarDTO>(car);
    }

    public async Task<CarDTO> Delete(Guid id)
    {
        Car car = await _unitOfWork.CarRepository.GetById(id);
        if(car is null) throw new RentCarException("Carro não encontrado");
        car.SetActive(false);
        _unitOfWork.CarRepository.Update(car);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CarDTO>(car);
    }
}
