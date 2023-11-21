using RentCar.Domain.Entities;
using RentCar.Domain.Exceptions;

namespace RentCar.Tests.Domain;

public class CarTests
{
    [Fact]
    public void CreateCar_WithValidData_ShouldNotThrowException()
    {
        // Arrange
        // Act
        Action act = () => new Car("Toyota", "Camry");

        // Assert
        Assert.Null(Record.Exception(act));
    }

    [Theory]
    [InlineData(null, "Camry")]
    [InlineData("Toyota", null)]
    [InlineData("", "Camry")]
    [InlineData("Toyota", "")]
    [InlineData(null, null)]
    [InlineData("", "")]
    public void CreateCar_WithInvalidData_ShouldThrowException(string brand, string model)
    {
        // Arrange
        // Act
        Action act = () => new Car(brand, model);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void BrandAndModel_ShouldBeTrimmed()
    {
        // Arrange
        string brand = "   Toyota   ";
        string model = "   Camry   ";

        // Act
        var car = new Car(brand, model);

        // Assert
        Assert.Equal("Toyota", car.Brand);
        Assert.Equal("Camry", car.Model);
    }

    [Fact]
    public void CarIsActiveByDefault()
    {
        // Arrange
        // Act
        var car = new Car("Toyota", "Camry");

        // Assert
        Assert.True(car.Active);
    }

    [Fact]
    public void CreateCar_WithWhitespaceInBrandAndModel_ShouldTrimAndCreate()
    {
        // Arrange
        string brand = "   Toyota   ";
        string model = "   Camry   ";

        // Act
        var car = new Car(brand, model);

        // Assert
        Assert.Equal("Toyota", car.Brand);
        Assert.Equal("Camry", car.Model);
    }

    [Fact]
    public void CreateCar_WithValidData_ShouldSetProperties()
    {
        // Arrange
        string brand = "Toyota";
        string model = "Camry";

        // Act
        var car = new Car(brand, model);

        // Assert
        Assert.Equal(brand, car.Brand);
        Assert.Equal(model, car.Model);
    }

    [Fact]
    public void CreateCar_WithInvalidBrand_ShouldThrowException()
    {
        // Arrange
        string brand = null;
        string model = "Camry";

        // Act
        Action act = () => new Car(brand, model);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateCar_WithInvalidModel_ShouldThrowException()
    {
        // Arrange
        string brand = "Toyota";
        string model = null;

        // Act
        Action act = () => new Car(brand, model);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateCar_WithEmptyBrand_ShouldThrowException()
    {
        // Arrange
        string brand = "";
        string model = "Camry";

        // Act
        Action act = () => new Car(brand, model);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateCar_WithEmptyModel_ShouldThrowException()
    {
        // Arrange
        string brand = "Toyota";
        string model = "";

        // Act
        Action act = () => new Car(brand, model);

        // Assert
        Assert.Throws<RentCarException>(act);
    }

    [Fact]
    public void CreateCar_WithValidData_ShouldSetPropertiesAndNotThrowException()
    {
        // Arrange
        string brand = "Toyota";
        string model = "Camry";

        // Act
        var car = new Car(brand, model);

        // Assert
        Assert.Equal(brand, car.Brand);
        Assert.Equal(model, car.Model);
        Assert.True(car.Active);
    }
}

