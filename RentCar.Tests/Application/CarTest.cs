using AutoMapper;
using Moq;
using RentCar.Application.DTOs;
using RentCar.Application.Services;
using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;

namespace RentCar.Tests.Application
{
    public class TestableCar : Car
    {
        public TestableCar(string brand, string model) : base(brand, model) { }
    }

    public class CarTests
    {
        [Fact]
        public async Task GetAll_ReturnsMappedCarDTOs()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var carService = new CarService(unitOfWorkMock.Object, mapperMock.Object);
            var carFilter = new CarFilter();
            var cars = new List<Car> { new TestableCar("Toyota", "Camry") };
            var carDTOs = new List<CarDTO> { new CarDTO { Brand = "Toyota", Model = "Camry" } };

            unitOfWorkMock.Setup(u => u.CarRepository.GetAll(carFilter)).ReturnsAsync(cars);
            mapperMock.Setup(m => m.Map<IEnumerable<CarDTO>>(cars)).Returns(carDTOs);

            // Act
            var result = await carService.GetAll(carFilter);

            // Assert
            Assert.Equal(carDTOs, result);
        }

        [Fact]
        public async Task GetById_ReturnsMappedCarDTO()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var carService = new CarService(unitOfWorkMock.Object, mapperMock.Object);
            var carId = Guid.NewGuid();
            var car = new TestableCar("Toyota", "Camry");
            var carDTO = new CarDTO { Brand = "Toyota", Model = "Camry" };

            unitOfWorkMock.Setup(u => u.CarRepository.GetById(carId)).ReturnsAsync(car);
            mapperMock.Setup(m => m.Map<CarDTO>(car)).Returns(carDTO);

            // Act
            var result = await carService.GetById(carId);

            // Assert
            Assert.Equal(carDTO, result);
        }

        [Fact]
        public async Task Create_ReturnsMappedCarDTO()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var carService = new CarService(unitOfWorkMock.Object, mapperMock.Object);
            var carDTO = new CarDTO { Brand = "Toyota", Model = "Camry" };
            var car = new TestableCar(carDTO.Brand, carDTO.Model);

            unitOfWorkMock.Setup(u => u.CarRepository.Add(It.IsAny<Car>()));
            unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);
            mapperMock.Setup(m => m.Map<Car>(carDTO)).Returns(car);
            mapperMock.Setup(m => m.Map<CarDTO>(car)).Returns(carDTO);

            // Act
            var result = await carService.Create(carDTO);

            // Assert
            Assert.Equal(carDTO, result);
        }

        [Fact]
        public async Task Update_CarNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var carService = new CarService(unitOfWorkMock.Object, mapperMock.Object);
            var carDTO = new CarDTO { Id = Guid.NewGuid() };

            unitOfWorkMock.Setup(u => u.CarRepository.GetById(carDTO.Id)).ReturnsAsync((Car)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => carService.Update(carDTO));
        }

        [Fact]
        public async Task Delete_CarNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var carService = new CarService(unitOfWorkMock.Object, mapperMock.Object);
            var carId = Guid.NewGuid();

            unitOfWorkMock.Setup(u => u.CarRepository.GetById(carId)).ReturnsAsync((Car)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => carService.Delete(carId));
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoCarsExist()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var carService = new CarService(unitOfWorkMock.Object, mapperMock.Object);
            var carFilter = new CarFilter();

            unitOfWorkMock.Setup(u => u.CarRepository.GetAll(carFilter)).ReturnsAsync(new List<Car>());

            // Act
            var result = await carService.GetAll(carFilter);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ReturnsNull_WhenCarNotFound()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var carService = new CarService(unitOfWorkMock.Object, mapperMock.Object);
            var carId = Guid.NewGuid();

            unitOfWorkMock.Setup(u => u.CarRepository.GetById(carId)).ReturnsAsync((Car)null);

            // Act
            var result = await carService.GetById(carId);

            // Assert
            Assert.Null(result);
        }

    }
}
