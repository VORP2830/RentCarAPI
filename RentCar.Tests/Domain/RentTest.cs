using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;

namespace RentCar.Tests.Domain;
public class RentTests
{
    [Fact]
    public void CreateRent_WithValidData_ShouldNotThrowException()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "A";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Theory]
    [InlineData("B")]
    [InlineData("")]
    [InlineData(null)]
    public void CreateRent_WithInvalidOperation_ShouldThrowException(string invalidOperation)
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddMinutes(30);

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, invalidOperation);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateRent_WithPastRentDate_ShouldThrowException()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddHours(-2);
        string operation = "A";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateRent_WithFutureRentDateOver1Hour_ShouldThrowException()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddHours(2);
        string operation = "A";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateRent_WithValidData_ShouldSetProperties()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "A";

        // Act
        var rent = new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Equal(clientId, rent.ClientId);
        Assert.Equal(carId, rent.CarId);
        Assert.Equal(rentDate, rent.RentDate);
        Assert.Equal(operation, rent.Operation);
        Assert.True(rent.Active);
    }

    [Fact]
    public void CreateRent_WithEmptyClientId_ShouldThrowException()
    {
        // Arrange
        Guid clientId = Guid.Empty;
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "A";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateRent_WithEmptyCarId_ShouldThrowException()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.Empty;
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "A";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateRent_WithNullClient_ShouldThrowException()
    {
        // Arrange
        Guid clientId = Guid.Empty;
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "A";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateRent_WithNullCar_ShouldThrowException()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.Empty;
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "A";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateRent_WithDevolutionOperation_ShouldNotThrowException()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "D";

        // Act
        Action act = () => new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Fact]
    public void CreateRent_WithDevolutionOperation_ShouldSetProperties()
    {
        // Arrange
        Guid clientId = Guid.NewGuid();
        Guid carId = Guid.NewGuid();
        DateTime rentDate = DateTime.Now.AddMinutes(30);
        string operation = "D";

        // Act
        var rent = new Rent(clientId, carId, rentDate, operation);

        // Assert
        Assert.Equal(clientId, rent.ClientId);
        Assert.Equal(carId, rent.CarId);
        Assert.Equal(rentDate, rent.RentDate);
        Assert.Equal(operation, rent.Operation);
        Assert.True(rent.Active);
    }
}

