using AutoMapper;
using RentCar.Domain.Entities;

namespace RentCar.Application.DTOs.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Client, ClientDTO>().ReverseMap();
		CreateMap<Car, CarDTO>().ReverseMap();
		CreateMap<Rent, RentDTO>().ReverseMap();
	}
}


