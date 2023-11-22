using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using RentCar.Application.DTOs;
using RentCar.Application.Services;
using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;
using Xunit;

namespace RentCar.Tests.Application
{
    public class TestableRent : Rent
    {
        public TestableRent(Guid clientId, Guid carId, DateTime rentDate, string operation) : base(clientId, carId, rentDate, operation) { }
    }

    public class RentServiceTests
    {
        [Fact]
        public async Task GetAll_ReturnsMappedRentDTOs()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentFilter = new RentFilter();
            var rents = new List<Rent> { new TestableRent(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, "A") };
            var rentDTOs = new List<RentDTO> { new RentDTO { ClientId = rents[0].ClientId, CarId = rents[0].CarId, RentDate = rents[0].RentDate, Operation = rents[0].Operation } };

            unitOfWorkMock.Setup(u => u.RentRepository.GetAll(rentFilter)).ReturnsAsync(rents);
            mapperMock.Setup(m => m.Map<IEnumerable<RentDTO>>(rents)).Returns(rentDTOs);

            // Act
            var result = await rentService.GetAll(rentFilter);

            // Assert
            Assert.Equal(rentDTOs, result);
        }

        [Fact]
        public async Task GetById_ReturnsMappedRentDTO()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentId = Guid.NewGuid();
            var rent = new TestableRent(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, "A");
            var rentDTO = new RentDTO { ClientId = rent.ClientId, CarId = rent.CarId, RentDate = rent.RentDate, Operation = rent.Operation };

            unitOfWorkMock.Setup(u => u.RentRepository.GetById(rentId)).ReturnsAsync(rent);
            mapperMock.Setup(m => m.Map<RentDTO>(rent)).Returns(rentDTO);

            // Act
            var result = await rentService.GetById(rentId);

            // Assert
            Assert.Equal(rentDTO, result);
        }

        [Fact]
        public async Task Create_ReturnsMappedRentDTO()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentDTO = new RentDTO { ClientId = Guid.NewGuid(), CarId = Guid.NewGuid(), RentDate = DateTime.Now, Operation = "A" };
            var client = new Client("John Doe", "12345678901", new DateOnly(1990, 1, 1));
            var car = new Car("Toyota", "Camry");
            var rent = new TestableRent(rentDTO.ClientId, rentDTO.CarId, rentDTO.RentDate, rentDTO.Operation);

            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(rentDTO.ClientId)).ReturnsAsync(client);
            unitOfWorkMock.Setup(u => u.CarRepository.GetById(rentDTO.CarId)).ReturnsAsync(car);
            unitOfWorkMock.Setup(u => u.RentRepository.Add(It.IsAny<Rent>()));
            unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);
            mapperMock.Setup(m => m.Map<Rent>(rentDTO)).Returns(rent);
            mapperMock.Setup(m => m.Map<RentDTO>(rent)).Returns(rentDTO);

            // Act
            var result = await rentService.Create(rentDTO);

            // Assert
            Assert.Equal(rentDTO, result);
        }

        [Fact]
        public async Task Update_RentNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentDTO = new RentDTO { Id = Guid.NewGuid() };

            unitOfWorkMock.Setup(u => u.RentRepository.GetById(rentDTO.Id)).ReturnsAsync((Rent)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => rentService.Update(rentDTO));
        }

        [Fact]
        public async Task Delete_RentNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentId = Guid.NewGuid();

            unitOfWorkMock.Setup(u => u.RentRepository.GetById(rentId)).ReturnsAsync((Rent)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => rentService.Delete(rentId));
        }

        [Fact]
        public async Task Create_ClientNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentDTO = new RentDTO { ClientId = Guid.NewGuid(), CarId = Guid.NewGuid(), RentDate = DateTime.Now, Operation = "A" };

            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(rentDTO.ClientId)).ReturnsAsync((Client)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => rentService.Create(rentDTO));
        }

        [Fact]
        public async Task Create_CarNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentDTO = new RentDTO { ClientId = Guid.NewGuid(), CarId = Guid.NewGuid(), RentDate = DateTime.Now, Operation = "A" };

            var client = new Client("John Doe", "12345678901", new DateOnly(1990, 1, 1));
            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(rentDTO.ClientId)).ReturnsAsync(client);
            unitOfWorkMock.Setup(u => u.CarRepository.GetById(rentDTO.CarId)).ReturnsAsync((Car)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => rentService.Create(rentDTO));
        }

        [Fact]
        public async Task Update_ClientNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentDTO = new RentDTO { Id = Guid.NewGuid(), ClientId = Guid.NewGuid(), CarId = Guid.NewGuid(), RentDate = DateTime.Now, Operation = "A" };

            unitOfWorkMock.Setup(u => u.RentRepository.GetById(rentDTO.Id)).ReturnsAsync(new TestableRent(rentDTO.ClientId, rentDTO.CarId, DateTime.Now, "A"));
            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(rentDTO.ClientId)).ReturnsAsync((Client)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => rentService.Update(rentDTO));
        }

        [Fact]
        public async Task Update_CarNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var rentService = new RentService(unitOfWorkMock.Object, mapperMock.Object);
            var rentDTO = new RentDTO { Id = Guid.NewGuid(), ClientId = Guid.NewGuid(), CarId = Guid.NewGuid(), RentDate = DateTime.Now, Operation = "A" };

            unitOfWorkMock.Setup(u => u.RentRepository.GetById(rentDTO.Id)).ReturnsAsync(new TestableRent(rentDTO.ClientId, rentDTO.CarId, DateTime.Now, "A"));
            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(rentDTO.ClientId)).ReturnsAsync(new Client("John Doe", "12345678901", new DateOnly(1990, 1, 1)));
            unitOfWorkMock.Setup(u => u.CarRepository.GetById(rentDTO.CarId)).ReturnsAsync((Car)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => rentService.Update(rentDTO));
        }

    }
}
