using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;

namespace RentCar.Tests.Domain;

public class ClientTest
{
    [Fact]
    public void NewClient_ValidData_ShouldNotThrowException()
    {
        var name = "John Doe";
        var cpf = "12345678901";
        var birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-30));
        Assert.Null(Record.Exception(() => new Client(name, cpf, birthDay)));
    }

    [Fact]
    public void NewClient_InvalidNameLength_ShouldThrowException()
    {
        var name = "A";
        Assert.Throws<RentCarException>(() => new Client(name, "12345678901", DateOnly.FromDateTime(DateTime.Now)));
    }

    [Fact]
    public void NewClient_InvalidNameLengthExceedsMaximum_ShouldThrowException()
    {
        var name = new string('A', 51);
        Assert.Throws<RentCarException>(() => new Client(name, "12345678901", DateOnly.FromDateTime(DateTime.Now)));
    }

    [Fact]
    public void NewClient_InvalidCPFLength_ShouldThrowException()
    {
        var cpf = "123";
        Assert.Throws<RentCarException>(() => new Client("John Doe", cpf, DateOnly.FromDateTime(DateTime.Now)));
    }

    [Fact]
    public void NewClient_InvalidCPFFormat_ShouldThrowException()
    {
        var cpf = "abc45678901";
        Assert.Throws<RentCarException>(() => new Client("John Doe", cpf, DateOnly.FromDateTime(DateTime.Now)));
    }

    [Fact]
    public void NewClient_FutureBirthDay_ShouldThrowException()
    {
        var birthDay = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        Assert.Throws<RentCarException>(() => new Client("John Doe", "12345678901", birthDay));
    }

    [Fact]
    public void NewClient_ValidData_PropertiesSetCorrectly()
    {
        var name = "John Doe";
        var cpf = "12345678901";
        var birthDay = DateOnly.FromDateTime(DateTime.Now.AddYears(-30));
        var client = new Client(name, cpf, birthDay);
        Assert.Equal(name, client.Name);
        Assert.Equal(cpf, client.CPF);
        Assert.Equal(birthDay, client.BirthDay);
    }
}

