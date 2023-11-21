using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;

namespace RentCar.Tests.Domain;
public class ClientTests
{
    [Fact]
    public void CreateClient_WithValidData_ShouldNotThrowException()
    {
        // Arrange
        string name = "John Doe";
        string cpf = "12345678901";
        DateOnly birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));

        // Act
        Action act = () => new Client(name, cpf, birthDay);

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Theory]
    [InlineData("Jo", "12345678901", "1990-01-01")]
    [InlineData("John Doe", "123456789012", "1990-01-01")]
    [InlineData("John Doe", "abc45678901", "1990-01-01")]
    public void CreateClient_WithInvalidData_ShouldThrowException(string invalidName, string invalidCpf, string invalidBirthDay)
    {
        // Arrange
        DateOnly birthDay = DateOnly.Parse(invalidBirthDay);

        // Act
        Action act = () => new Client(invalidName, invalidCpf, birthDay);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void Name_ShouldBeTrimmed()
    {
        // Arrange
        string name = "   John Doe   ";
        string cpf = "12345678901";
        DateOnly birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));

        // Act
        var client = new Client(name, cpf, birthDay);

        // Assert
        Assert.Equal("John Doe", client.Name);
    }

    [Fact]
    public void CreateClient_WithInvalidName_ShouldThrowException()
    {
        // Arrange
        string invalidName = "Jo";
        string cpf = "12345678901";
        DateOnly birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));

        // Act
        Action act = () => new Client(invalidName, cpf, birthDay);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateClient_WithInvalidCpf_ShouldThrowException()
    {
        // Arrange
        string name = "John Doe";
        string invalidCpf = "abc45678901";
        DateOnly birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));

        // Act
        Action act = () => new Client(name, invalidCpf, birthDay);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateClient_WithEmptyName_ShouldThrowException()
    {
        // Arrange
        string emptyName = "";
        string cpf = "12345678901";
        DateOnly birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));

        // Act
        Action act = () => new Client(emptyName, cpf, birthDay);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateClient_WithEmptyCpf_ShouldThrowException()
    {
        // Arrange
        string name = "John Doe";
        string emptyCpf = "";
        DateOnly birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));

        // Act
        Action act = () => new Client(name, emptyCpf, birthDay);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateClient_WithFutureBirthDay_ShouldThrowException()
    {
        // Arrange
        string name = "John Doe";
        string cpf = "12345678901";
        DateOnly futureBirthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(1));

        // Act
        Action act = () => new Client(name, cpf, futureBirthDay);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateClient_WithValidData_ShouldSetProperties()
    {
        // Arrange
        string name = "John Doe";
        string cpf = "12345678901";
        DateOnly birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-25));

        // Act
        var client = new Client(name, cpf, birthDay);

        // Assert
        Assert.Equal(name, client.Name);
        Assert.Equal(cpf, client.CPF);
        Assert.Equal(birthDay, client.BirthDay);
        Assert.True(client.Active);
    }
}

