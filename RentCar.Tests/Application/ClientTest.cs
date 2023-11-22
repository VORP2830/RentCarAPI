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
    public class TestableClient : Client
    {
        public TestableClient(string name, string cpf, DateOnly birthDay) : base(name, cpf, birthDay) { }
    }

    public class ClientTests
    {
        [Fact]
        public async Task GetAll_ReturnsMappedClientDTOs()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var clientService = new ClientService(unitOfWorkMock.Object, mapperMock.Object);
            var clientFilter = new ClientFilter();
            var clients = new List<Client> { new TestableClient("John Doe", "12345678901", new DateOnly(1990, 1, 1)) };
            var clientDTOs = new List<ClientDTO> { new ClientDTO { Name = "John Doe", CPF = "12345678901", BirthDay = new DateOnly(1990, 1, 1) } };

            unitOfWorkMock.Setup(u => u.ClientRepository.GetAll(clientFilter)).ReturnsAsync(clients);
            mapperMock.Setup(m => m.Map<IEnumerable<ClientDTO>>(clients)).Returns(clientDTOs);

            // Act
            var result = await clientService.GetAll(clientFilter);

            // Assert
            Assert.Equal(clientDTOs, result);
        }

        [Fact]
        public async Task GetById_ReturnsMappedClientDTO()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var clientService = new ClientService(unitOfWorkMock.Object, mapperMock.Object);
            var clientId = Guid.NewGuid();
            var client = new TestableClient("John Doe", "12345678901", new DateOnly(1990, 1, 1));
            var clientDTO = new ClientDTO { Name = "John Doe", CPF = "12345678901", BirthDay = new DateOnly(1990, 1, 1) };

            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(clientId)).ReturnsAsync(client);
            mapperMock.Setup(m => m.Map<ClientDTO>(client)).Returns(clientDTO);

            // Act
            var result = await clientService.GetById(clientId);

            // Assert
            Assert.Equal(clientDTO, result);
        }

        [Fact]
        public async Task Create_ReturnsMappedClientDTO()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var clientService = new ClientService(unitOfWorkMock.Object, mapperMock.Object);
            var clientDTO = new ClientDTO { Name = "John Doe", CPF = "12345678901", BirthDay = new DateOnly(1990, 1, 1) };
            var client = new TestableClient(clientDTO.Name, clientDTO.CPF, clientDTO.BirthDay);

            unitOfWorkMock.Setup(u => u.ClientRepository.Add(It.IsAny<Client>()));
            unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);
            mapperMock.Setup(m => m.Map<Client>(clientDTO)).Returns(client);
            mapperMock.Setup(m => m.Map<ClientDTO>(client)).Returns(clientDTO);

            // Act
            var result = await clientService.Create(clientDTO);

            // Assert
            Assert.Equal(clientDTO, result);
        }

        [Fact]
        public async Task Update_ClientNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var clientService = new ClientService(unitOfWorkMock.Object, mapperMock.Object);
            var clientDTO = new ClientDTO { Id = Guid.NewGuid() };

            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(clientDTO.Id)).ReturnsAsync((Client)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => clientService.Update(clientDTO));
        }

        [Fact]
        public async Task Delete_ClientNotFound_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var clientService = new ClientService(unitOfWorkMock.Object, mapperMock.Object);
            var clientId = Guid.NewGuid();

            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(clientId)).ReturnsAsync((Client)null);

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => clientService.Delete(clientId));
        }

        // ... (código anterior)

        [Fact]
        public async Task Create_ClientWithExistingCPF_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var clientService = new ClientService(unitOfWorkMock.Object, mapperMock.Object);
            var clientDTO = new ClientDTO { CPF = "12345678901" };
            var existingClient = new Client("John Doe", "12345678901", new DateOnly(1990, 1, 1));

            unitOfWorkMock.Setup(u => u.ClientRepository.GetAll(It.IsAny<ClientFilter>())).ReturnsAsync(new List<Client> { existingClient });

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => clientService.Create(clientDTO));
        }

        [Fact]
        public async Task Update_ClientWithExistingCPF_ThrowsRentCarException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var clientService = new ClientService(unitOfWorkMock.Object, mapperMock.Object);
            var clientDTO = new ClientDTO { Id = Guid.NewGuid(), CPF = "12345678901" };
            var existingClient = new Client("John Doe", "12345678901", new DateOnly(1990, 1, 1));

            unitOfWorkMock.Setup(u => u.ClientRepository.GetById(clientDTO.Id)).ReturnsAsync(existingClient);
            unitOfWorkMock.Setup(u => u.ClientRepository.GetAll(It.IsAny<ClientFilter>())).ReturnsAsync(new List<Client> { existingClient });

            // Act and Assert
            await Assert.ThrowsAsync<RentCarException>(() => clientService.Update(clientDTO));
        }

    }
}
